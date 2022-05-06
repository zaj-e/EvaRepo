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
public class DataController : ControllerBase
{
        private readonly IMapper _mapper;
        private readonly IDataService _dataService;

        public DataController(IMapper mapper, IDataService dataService)
        {
            _mapper = mapper;
            _dataService = dataService;
        }
        
        [SwaggerOperation(
            Summary = "",
            Description = "",
            Tags = new[] { "Data" }
        )]
        [SwaggerResponse(200, "", typeof(Data))]
        [HttpGet("{dataId:int}")]
        public async Task<IActionResult> GetByIdAsync(int dataId)
        {
            var result = await _dataService.SearchByIdAsync(dataId);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Resource);
        }
        
        [SwaggerOperation(
            Summary = "",
            Description = "",
            Tags = new[] { "Data" }
        )]
        [SwaggerResponse(200, "", typeof(Data))]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Data resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
        
            var data = _mapper.Map<Data, Data>(resource);
            var result = await _dataService.CreateAsync(data);
        
            if (!result.Success)
                return BadRequest(result.Message);
        
            return Ok(result.Resource);
        }
        
        [SwaggerOperation(
            Summary = "",
            Tags = new[] { "Data" }
        )]
        [SwaggerResponse(200, "", typeof(Data))]
        [HttpPatch("{accountId}")]
        public async Task<IActionResult> UpdateAsync(Data data, string accountId)
        {
            var result = await _dataService.Update(data);

            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Resource);
        }


        [SwaggerOperation(
            Summary = "",
            Tags = new[] { "Data" })
        ]

        [SwaggerResponse(200, "", typeof(Data))]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _dataService.DeleteByIdAsync(id);
            return Ok(result);
        }
}