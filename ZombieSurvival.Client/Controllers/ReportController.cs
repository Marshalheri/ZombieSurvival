using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZombieSurvival.Core;
using ZombieSurvival.Core.DTOs;

namespace ZombieSurvival.Client.Controllers
{
    [Route("api/v1/report")]
    [ApiController]
    public class ReportController : RootController
    {
        private readonly IReportService _service;
        public ReportController(IReportService service)
        {
            _service = service;
        }
        /// <summary>
        /// Get the percentage of infected or non-infected survivors
        /// </summary>
        /// <returns></returns>
        [HttpGet("infection/percentage")]
        [ProducesResponseType(typeof(GetInfectionPercentageResponseDTO), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetInfectionPercentage()
        {
            var result = await _service.GetInfectionPercentageAsync();

            if (!result.IsSuccessful)
            {
                return CreateResponse(result.Error, result.FaultType);
            }
            return Ok(result.GetPayload());
        }
        
        /// <summary>
        /// Get total points lost because of infection
        /// </summary>
        /// <returns></returns>
        [HttpGet("points/lost")]
        [ProducesResponseType(typeof(GetInfectionPercentageResponseDTO), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPointsLost()
        {
            var result = await _service.GetPointsLostAsync();

            if (!result.IsSuccessful)
            {
                return CreateResponse(result.Error, result.FaultType);
            }
            return Ok(result.GetPayload());
        }
    }
}
