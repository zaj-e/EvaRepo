using AutoMapper;
using FirelessApi.Controllers.DataObjects;
using FirelessApi.Domain;
using FirelessApi.Domain.Services;
using FirelessApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FirelessApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlertsController : ControllerBase
{
        private readonly IMapper _mapper;
        private readonly IAlertsService _alertService;

        public AlertsController(IMapper mapper, IAlertsService alertService)
        {
            _mapper = mapper;
            _alertService = alertService;
        }
        
        [SwaggerOperation(
            Summary = "",
            Description = "",
            Tags = new[] { "Alerts" }
        )]
        [SwaggerResponse(200, "", typeof(Alert))]
        [HttpGet("{alertId:int}")]
        public async Task<IActionResult> GetByIdAsync(int alertId)
        {
            var result = await _alertService.SearchByIdAsync(alertId);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Resource);
        }
        
        [SwaggerOperation(
            Summary = "",
            Description = "",
            Tags = new[] { "Alerts" }
        )]
        [SwaggerResponse(200, "", typeof(Alert))]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Alert resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
        
            var alert = _mapper.Map<Alert, Alert>(resource);
            var result = await _alertService.CreateAsync(alert);
        
            if (!result.Success)
                return BadRequest(result.Message);
        
            return Ok(result.Resource);
        }
        
        [SwaggerOperation(
            Summary = "",
            Tags = new[] { "Alerts" }
        )]
        [SwaggerResponse(200, "", typeof(Alert))]
        [HttpPatch("{accountId}")]
        public async Task<IActionResult> UpdateAsync(Alert alert, string accountId)
        {
            var result = await _alertService.Update(alert);

            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Resource);
        }


        [SwaggerOperation(
            Summary = "",
            Tags = new[] { "Alerts" })
        ]

        [SwaggerResponse(200, "", typeof(Alert))]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _alertService.DeleteByIdAsync(id);
            return Ok(result);
        }
}