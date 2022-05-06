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
public class UsersController : ControllerBase
{
        private readonly IMapper _mapper;
        private readonly IUsersService _userService;

        public UsersController(IMapper mapper, IUsersService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        
        [SwaggerOperation(
            Summary = "",
            Description = "",
            Tags = new[] { "Users" }
        )]
        [SwaggerResponse(200, "", typeof(User))]
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetByIdAsync(int userId)
        {
            var result = await _userService.SearchByIdAsync(userId);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Resource);
        }
        
        [SwaggerOperation(
            Summary = "",
            Description = "",
            Tags = new[] { "Users" }
        )]
        [SwaggerResponse(200, "", typeof(User))]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] NewUser resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
        
            var user = _mapper.Map<NewUser, User>(resource);
            var result = await _userService.CreateAsync(user);
        
            if (!result.Success)
                return BadRequest(result.Message);
        
            return Ok(result.Resource);
        }
        
        [SwaggerOperation(
            Summary = "",
            Tags = new[] { "Users" }
        )]
        [SwaggerResponse(200, "", typeof(User))]
        [HttpPatch("{accountId}")]
        public async Task<IActionResult> UpdateAsync(User user, string accountId)
        {
            var result = await _userService.Update(user);

            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Resource);
        }


        [SwaggerOperation(
            Summary = "",
            Tags = new[] { "Users" })
        ]

        [SwaggerResponse(200, "", typeof(User))]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _userService.DeleteByIdAsync(id);
            return Ok(result);
        }
}