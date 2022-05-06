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
public class RegionsController : ControllerBase
{
        private readonly IMapper _mapper;
        private readonly IRegionsService _regionService;

        public RegionsController(IMapper mapper, IRegionsService regionService)
        {
            _mapper = mapper;
            _regionService = regionService;
        }
        
        [SwaggerOperation(
            Summary = "",
            Description = "",
            Tags = new[] { "Regions" }
        )]
        [SwaggerResponse(200, "", typeof(Region))]
        [HttpGet("{regionId:int}")]
        public async Task<IActionResult> GetByIdAsync(int regionId)
        {
            var result = await _regionService.SearchByIdAsync(regionId);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Resource);
        }
        
        [SwaggerOperation(
            Summary = "",
            Description = "",
            Tags = new[] { "Regions" }
        )]
        [SwaggerResponse(200, "", typeof(Region))]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Region resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
        
            var region = _mapper.Map<Region, Region>(resource);
            var result = await _regionService.CreateAsync(region);
        
            if (!result.Success)
                return BadRequest(result.Message);
        
            return Ok(result.Resource);
        }
        
        [SwaggerOperation(
            Summary = "",
            Tags = new[] { "Regions" }
        )]
        [SwaggerResponse(200, "", typeof(Region))]
        [HttpPatch("{accountId}")]
        public async Task<IActionResult> UpdateAsync(Region region, string accountId)
        {
            var result = await _regionService.Update(region);

            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Resource);
        }


        [SwaggerOperation(
            Summary = "",
            Tags = new[] { "Regions" })
        ]

        [SwaggerResponse(200, "", typeof(Region))]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _regionService.DeleteByIdAsync(id);
            return Ok(result);
        }
}