using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectY.Middleware.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZombieSurvival.Core;
using ZombieSurvival.Core.DTOs;

namespace ZombieSurvival.Client.Controllers
{
    [Route("api/v1/survivor")]
    [ApiController]
    public class SurvivorController : RootController
    {
        private readonly ISurvivorService _service;
        public SurvivorController(ISurvivorService service)
        {
            _service = service;
        }

        /// <summary>
        /// Profile Survivor
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("profile")]
        [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> ProfileSurvivor([FromBody] ProfileSurvivorRequestDTO request)
        {
            var result = await _service.ProfileSurvivorAsync(request);

            if (!result.IsSuccessful)
            {
                return CreateResponse(result.Error, result.FaultType);
            }
            return Ok();
        }
        
        /// <summary>
        /// Update Survivor Location
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("location/update")]
        [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> UpdateSurvivorLocation([FromBody] UpdateSurvivorLocationRequestDTO request)
        {
            var result = await _service.UpdateSurvivorLocationAsync(request);

            if (!result.IsSuccessful)
            {
                return CreateResponse(result.Error, result.FaultType);
            }
            return Ok();
        }
        
        /// <summary>
        /// Report Contaminated Survivor
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        [HttpPost("report/contaminated")]
        [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> ReportContaminatedSurvivor([FromQuery] string Username)
        {
            var result = await _service.ReportContaminatedSurvivorAsync(Username);

            if (!result.IsSuccessful)
            {
                return CreateResponse(result.Error, result.FaultType);
            }
            return Ok();
        }

        /// <summary>
        /// Trade items 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("trade")]
        [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> ReportContaminatedSurvivor([FromBody] TradeRequestDTO request)
        {
            var result = await _service.TradeAsync(request);

            if (!result.IsSuccessful)
            {
                return CreateResponse(result.Error, result.FaultType);
            }
            return Ok();
        }
        
        /// <summary>
        /// Get Survivor details
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("details")]
        [ProducesResponseType(typeof(GetSurvivorResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetSurvivor([FromQuery] string username)
        {
            var result = await _service.GetSurvivorAsync(username);

            if (!result.IsSuccessful)
            {
                return CreateResponse(result.Error, result.FaultType);
            }
            return Ok(result.GetPayload());
        }
    }
}
