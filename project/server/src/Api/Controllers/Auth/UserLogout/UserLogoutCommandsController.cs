// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Interfaces.Auth.UserLogouts;
using server.src.WebApi.Controllers;

namespace server.src.Api.Controllers.Auth.UserLogout;

[Route("api/auth/userLogouts")]
[ApiController]
public class UserLogoutCommandsController : BaseApiController
{
    private readonly IUserLogoutCommands _userLogoutCommands;
    
    public UserLogoutCommandsController(IUserLogoutCommands userLogoutCommands)
    {
        _userLogoutCommands = userLogoutCommands;
    }

    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [Route("user/{id}")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "User logout", Description = "Logs out the current user.")]
    [SwaggerResponse(StatusCodes.Status200OK, "User logged out successfully")]
    public async Task<IActionResult> Logout(Guid id, CancellationToken token)
        => Ok(await _userLogoutCommands.LogoutUserService(id, token));

}
