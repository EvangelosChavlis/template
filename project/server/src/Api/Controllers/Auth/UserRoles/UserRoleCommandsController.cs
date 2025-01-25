// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Domain.Dto.Common;
using server.src.WebApi.Controllers;
using server.src.Application.Interfaces.Auth.UserRoles;

namespace server.src.Api.Controllers.Auth.Users;

[Route("api/auth/userRoles")]
[ApiController]
public class UserRoleCommandsController : BaseApiController
{
    private readonly IUserRoleCommands _userRoleCommands;
    
    public UserRoleCommandsController(IUserRoleCommands userRoleCommands)
    {
        _userRoleCommands = userRoleCommands;
    }
    

    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [Route("assign/{userId}/{roleId}")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(Summary = "Assign a role to a user", Description = "Assigns a specific role to a user by their unique user ID and role ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Role assigned to user successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User or role not found")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid assignment request")]
    public async Task<IActionResult> AssignRoleToUser(Guid userId, Guid roleId, CancellationToken token)
        => Ok(await _userRoleCommands.AssignRoleToUserService(userId, roleId, token));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [Route("unassign/{userId}/{roleId}")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(Summary = "Unassign a role from a user", Description = "Unassigns a specific role from a user by their unique user ID and role ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Role unassigned to user successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User or role not found")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid assignment request")]
    public async Task<IActionResult> UnassignRoleFromUser(Guid userId, Guid roleId, CancellationToken token)
        => Ok(await _userRoleCommands.UnassignRoleFromUserService(userId, roleId, token));
}