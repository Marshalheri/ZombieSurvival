using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProjectY.Middleware.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieSurvival.Core.DTOs;
using ZombieSurvival.Core.Models;
using ZombieSurvival.Core.Repository;
using ZombieSurvival.Core.Services;

namespace ZombieSurvival.Core.Implementations
{
    public class SurvivorService : ISurvivorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SurvivorService> _logger;
        private readonly SystemSettings _settings;
        public SurvivorService(IUnitOfWork unitOfWork, ILogger<SurvivorService> logger, IOptions<SystemSettings> settings)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _settings = settings.Value;
        }

        public async Task<PayloadResponse<GetSurvivorResponseDTO>> GetSurvivorAsync(string username)
        {
            var response = new PayloadResponse<GetSurvivorResponseDTO>(false);

            var user = await _unitOfWork.SurvivorRepository.GetAsync(x => x.Username == username && x.IsInfected == false, includes: c => c.Inventories);
            if (user == null)
            {
                return ErrorResponse.Create<PayloadResponse<GetSurvivorResponseDTO>>(FaultMode.REQUESTED_ENTITY_NOT_FOUND, "E003", "Username not found");
            }
            var payload = new GetSurvivorResponseDTO
            {
                Name = user.Name,
                Username = user.Username,
                Age = user.Age,
                Gender = user.Gender,
                Latitude = user.Latitude,
                Longitude = user.Longitude,
                ContaminationCount = user.ContaminationCount,
                Inventory = user.Inventories.Select(x => new SurvivorInventoryDTO { Name = x.Name, Point = x.Point }).ToList()
            };

            response.SetPayload(payload);
            response.IsSuccessful = true;

            return response;
        }

        public async Task<BasicResponse> ProfileSurvivorAsync(ProfileSurvivorRequestDTO request)
        {
            _logger.LogInformation($"{nameof(request)} => {Util.SerializeAsJson(request)}");
            var response = new BasicResponse(false);
            if (!request.IsValid(out var problemSource))
            {
                return ErrorResponse.Create<BasicResponse>(
                        FaultMode.CLIENT_INVALID_ARGUMENT,
                        "E001",
                        $"Invalid input parameter - {problemSource}");
            };

            var user = await _unitOfWork.SurvivorRepository.GetAllAsync(x => x.Username == request.Username && x.IsInfected == false);
            if (user.Any())
            {
                return ErrorResponse.Create<BasicResponse>(FaultMode.CLIENT_INVALID_ARGUMENT, "E002", "Username already registered");
            }

            var survivor = new Survivor
            {
                Age = request.Age,
                Gender = request.Gender.ToString(),
                Name = $"{request.Firstname} {request.Lastname}",
                Username = request.Username,
                Latitude = request.Location.Latitude,
                Longitude = request.Location.Longitude,
                ContaminationCount = 0,
                IsInfected = false,
                DateCreated = DateTime.Now
            };
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.SurvivorRepository.AddAsync(survivor);
                await _unitOfWork.SaveAsync();

                var dateCreated = DateTime.Now;
                var inventories = new List<Inventory>();
                foreach (var item in request.SurvivorInventories)
                {
                    var inventory = new Inventory
                    {
                        SurvivorId = survivor.Id,
                        IsActive = true,
                        Name = item.InventoryItem.ToString(),
                        Point = item.Point,
                        DateCreated = dateCreated
                    };
                    inventories.Add(inventory);
                }

                await _unitOfWork.InventoryRepository.AddRangeAsync(inventories);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitAsync();

                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR PROFILING SURVIVOR");
                throw new Exception("ERROR PROFILING SURVIVOR");
            }

            return response;
        }

        public async Task<BasicResponse> ReportContaminatedSurvivorAsync(string username)
        {
            var response = new BasicResponse(false);

            var user = await _unitOfWork.SurvivorRepository.GetAsync(x => x.Username == username && x.IsInfected == false);
            if (user == null)
            {
                return ErrorResponse.Create<BasicResponse>(FaultMode.REQUESTED_ENTITY_NOT_FOUND, "E003", "Username not found");
            }

            var dateUpdated = DateTime.Now;
            user.ContaminationCount++;
            user.IsInfected = user.ContaminationCount >= _settings.ContaminationMaxCount;
            user.DateUpdated = dateUpdated;

            await _unitOfWork.BeginTransactionAsync();
            _unitOfWork.SurvivorRepository.Update(user);
            await _unitOfWork.SaveAsync();

            if (user.IsInfected)
            {
                var inventories = await _unitOfWork.InventoryRepository.GetAllAsync(x => x.SurvivorId == user.Id);
                foreach (var item in inventories)
                {
                    item.IsActive = false;
                    item.DateUpdated = dateUpdated;
                }

                _unitOfWork.InventoryRepository.UpdateRangeAsync(inventories);
                await _unitOfWork.SaveAsync();
            }
            await _unitOfWork.CommitAsync();

            response.IsSuccessful = true;
            return response;
        }

        public async Task<BasicResponse> TradeAsync(TradeRequestDTO request)
        {
            _logger.LogInformation($"{nameof(request)} => {Util.SerializeAsJson(request)}");
            var response = new BasicResponse(false);
            if (!request.IsValid(out var problemSource))
            {
                return ErrorResponse.Create<BasicResponse>(
                        FaultMode.CLIENT_INVALID_ARGUMENT,
                        "E001",
                        $"Invalid input parameter - {problemSource}");
            };

            var buyer = await _unitOfWork.SurvivorRepository.GetAsync(x => x.Username == request.BuyerUsername && x.IsInfected == false, includes: c => c.Inventories);
            if (buyer == null)
            {
                return ErrorResponse.Create<BasicResponse>(FaultMode.REQUESTED_ENTITY_NOT_FOUND, "E003", $"Username not found {request.BuyerUsername}");
            }

            var seller = await _unitOfWork.SurvivorRepository.GetAsync(x => x.Username == request.SellerUsername && x.IsInfected == false, includes: c => c.Inventories);
            if (seller == null)
            {
                return ErrorResponse.Create<BasicResponse>(FaultMode.REQUESTED_ENTITY_NOT_FOUND, "E003", $"Username not found {request.SellerUsername}");
            }

            var prices = await _unitOfWork.PriceRepository.GetAllAsync();
            if (prices.Count < 1)
            {
                return ErrorResponse.Create<BasicResponse>(FaultMode.REQUESTED_ENTITY_NOT_FOUND, "E004", "Prices not found");
            }

            var dateUpdated = DateTime.Now;

            var buyingPoints = 0;
            foreach (var item in request.BuyerTradeItems)
            {
                var price = prices.FirstOrDefault(x => x.Name == item.Item.ToString());
                var point = (price.Point * item.Quantity) / price.Quantity;
                buyingPoints += point;

                var buyerItem = buyer.Inventories.FirstOrDefault(x => x.Name == item.Item.ToString());
                if (buyerItem == null)
                {
                    return ErrorResponse.Create<BasicResponse>(FaultMode.CLIENT_INVALID_ARGUMENT,
                            "E005", $"{request.BuyerUsername} does not have any {item.Item} to complete the transaction");
                }
                buyerItem.Point += point;
                buyerItem.DateUpdated = dateUpdated;
            }

            var sellingPoints = 0;
            foreach (var item in request.SellerTradeItems)
            {
                var price = prices.FirstOrDefault(x => x.Name == item.Item.ToString());
                var point = (price.Point * item.Quantity) / price.Quantity;
                sellingPoints += point;

                var sellerItem = seller.Inventories.FirstOrDefault(x => x.Name == item.Item.ToString());
                if (sellerItem == null)
                {
                    return ErrorResponse.Create<BasicResponse>(FaultMode.CLIENT_INVALID_ARGUMENT,
                            "E005", $"{request.SellerUsername} does not have any {item.Item} to complete the transaction");
                }

                if (sellerItem.Point < point)
                {
                    return ErrorResponse.Create<BasicResponse>(FaultMode.CLIENT_INVALID_ARGUMENT,
                            "E005", $"{request.SellerUsername} does not have enough {item.Item.ToString()} to complete the transaction");
                }
                sellerItem.Point -= point;
                sellerItem.DateUpdated = dateUpdated;
            }

            if (buyingPoints != sellingPoints)
            {
                return ErrorResponse.Create<BasicResponse>(FaultMode.REQUESTED_ENTITY_NOT_FOUND, "E006", "Trade amounts not balanced");
            }

            await _unitOfWork.BeginTransactionAsync();
            _unitOfWork.SurvivorRepository.Update(buyer);
            _unitOfWork.SurvivorRepository.Update(seller);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitAsync();

            response.IsSuccessful = true;
            return response;
        }

        public async Task<BasicResponse> UpdateSurvivorLocationAsync(UpdateSurvivorLocationRequestDTO request)
        {
            _logger.LogInformation($"{nameof(request)} => {Util.SerializeAsJson(request)}");
            var response = new BasicResponse(false);
            if (!request.IsValid(out var problemSource))
            {
                return ErrorResponse.Create<BasicResponse>(
                        FaultMode.CLIENT_INVALID_ARGUMENT,
                        "E001",
                        $"Invalid input parameter - {problemSource}");
            };

            var user = await _unitOfWork.SurvivorRepository.GetAsync(x => x.Username == request.Username && x.IsInfected == false);
            if (user == null)
            {
                return ErrorResponse.Create<BasicResponse>(FaultMode.REQUESTED_ENTITY_NOT_FOUND, "E003", "Username not found");
            }

            user.Latitude = request.Location.Latitude;
            user.Longitude = request.Location.Longitude;
            user.DateUpdated = DateTime.Now;
            _unitOfWork.SurvivorRepository.Update(user);
            await _unitOfWork.SaveAsync();

            response.IsSuccessful = true;
            return response;
        }
    }
}
