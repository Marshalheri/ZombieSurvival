using ProjectY.Middleware.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieSurvival.Core.DTOs;

namespace ZombieSurvival.Core
{
    public interface ISurvivorService
    {
        Task<BasicResponse> ProfileSurvivorAsync(ProfileSurvivorRequestDTO request);
        Task<BasicResponse> UpdateSurvivorLocationAsync(UpdateSurvivorLocationRequestDTO request);
        Task<BasicResponse> ReportContaminatedSurvivorAsync(string username);
        Task<BasicResponse> TradeAsync(TradeRequestDTO request);
        Task<PayloadResponse<GetSurvivorResponseDTO>> GetSurvivorAsync(string username);
    }
}
