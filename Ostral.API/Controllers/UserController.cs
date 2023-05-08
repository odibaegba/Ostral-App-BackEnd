using Microsoft.AspNetCore.Mvc;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;

namespace Ostral.API.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> UserProfile(string id)
    {
        var response = await _userService.GetUser(id);

        if (response.Errors.Any())
            return BadRequest(ResponseDTO<object>.Fail(response.Errors));

        return Ok(ResponseDTO<object>.Success(response.Data!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserProfile(string id, [FromBody] UpdateUserDTO updateUserDTO)
    {
        var response = await _userService.UpdateUserProfile(id, updateUserDTO);

        if (response.Errors.Any())
            return BadRequest(ResponseDTO<object>.Fail(response.Errors));

        return Ok(ResponseDTO<object>.Success(response.Data!));
    }
}