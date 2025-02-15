// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Api.Controllers;
using server.src.Application.Auth.UserRoles.Interfaces;
using server.src.Domain.Common.Dtos;

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
    {
        var result = await _userRoleCommands.AssignRoleToUserAsync(userId, roleId, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [Route("unassign/{userId}/{roleId}")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(Summary = "Unassign a role from a user", Description = "Unassigns a specific role from a user by their unique user ID and role ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Role unassigned to user successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User or role not found")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid assignment request")]
    public async Task<IActionResult> UnassignRoleFromUser(Guid userId, Guid roleId, CancellationToken token)
    {
        var result = await _userRoleCommands.UnassignRoleFromUserAsync(userId, roleId, token);
        return StatusCode(result.StatusCode, result);
    }
}