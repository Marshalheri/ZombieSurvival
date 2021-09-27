using ProjectY.Middleware.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieSurvival.Core.DTOs;

namespace ZombieSurvival.Core
{
    public interface IReportService
    {
        Task<PayloadResponse<GetInfectionPercentageResponseDTO>> GetInfectionPercentageAsync();
        Task<PayloadResponse<GetPointsLostResponseDTO>> GetPointsLostAsync();
    }
}
