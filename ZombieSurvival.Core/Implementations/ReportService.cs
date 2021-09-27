using Microsoft.Extensions.Logging;
using ProjectY.Middleware.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieSurvival.Core.DTOs;
using ZombieSurvival.Core.Repository;

namespace ZombieSurvival.Core.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ReportService> _logger;
        public ReportService(IUnitOfWork unitOfWork, ILogger<ReportService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<PayloadResponse<GetInfectionPercentageResponseDTO>> GetInfectionPercentageAsync()
        {
            var response = new PayloadResponse<GetInfectionPercentageResponseDTO>(true);
            var totalUsers = await _unitOfWork.SurvivorRepository.CountAll();
            var infectedCount = await _unitOfWork.SurvivorRepository.CountWhere(x => x.IsInfected == true);
            var nonInfectedCount = await _unitOfWork.SurvivorRepository.CountWhere(x => x.IsInfected == false);
            response.SetPayload(new GetInfectionPercentageResponseDTO
            {
                InfectedPercentage = $"{((infectedCount * 100) / (totalUsers * 100)) * 100}%",
                NonInfectedPercentage = $"{((nonInfectedCount * 100) / (totalUsers * 100)) * 100}%"
            });
            return response;
        }

        public async Task<PayloadResponse<GetPointsLostResponseDTO>> GetPointsLostAsync()
        {
            var response = new PayloadResponse<GetPointsLostResponseDTO>(true);
            var inventories = await _unitOfWork.InventoryRepository.GetAllAsync();
            var cummulativePoints = inventories.Sum(x => x.Point);
            var pointsLost = inventories.Where(x => x.IsActive == false).Sum(x => x.Point);
            response.SetPayload(new GetPointsLostResponseDTO
            {
                CummulativePoints = cummulativePoints,
                PointsLost = pointsLost
            });
            return response;
        }
    }
}
