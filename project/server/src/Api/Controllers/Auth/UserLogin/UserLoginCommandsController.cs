// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.WebApi.Controllers;
using server.src.Application.Auth.UserLogins.Interfaces;

namespace server.src.Api.Controllers.Auth.UserLogin;

[Route("api/auth/userLogins")]
[ApiController]
public class UserLoginCommandsController : BaseApiController
{
    private readonly IUserLoginCommands _userLoginCommands;
    
    public UserLoginCommandsController(IUserLoginCommands userLoginCommands)
    {
        _userLoginCommands = userLoginCommands;
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "User login", Description = "Logs in a user with the provided credentials.")]
    [SwaggerResponse(StatusCodes.Status200OK, "User logged in successfully", typeof(Response<AuthenticatedUserDto>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Invalid credentials")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto, CancellationToken token)
    {
        var result = await _userLoginCommands.LoginUserAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }
}
