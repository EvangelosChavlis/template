// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.WebApi.Controllers;
using server.src.Application.Auth.Roles.Interfaces;

namespace server.src.Api.Controllers.Auth.Roles;

[Route("api/auth/roles")]
[ApiController]
[Authorize(Roles = "Administrator")]
public class RoleCommandsController : BaseApiController
{
    private readonly IRoleCommands _roleCommands;
    
    public RoleCommandsController(IRoleCommands roleCommands)
    {
        _roleCommands = roleCommands;
    }

    [ApiExplorerSettings(GroupName = "auth")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new role", Description = "Creates a new role using the provided role data.")]
    [SwaggerResponse(StatusCodes.Status201Created, "Role created successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid role data")]
    public async Task<IActionResult> CreateRole([FromBody] RoleDto dto, CancellationToken token)
    {
        var result = await _roleCommands.CreateRoleAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpPost("initialize")]
    [SwaggerOperation(Summary = "Initialize roles", Description = "Initializes multiple roles using a list of role data.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Roles initialized successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid roles data")]
    public async Task<IActionResult> InitializeRole([FromBody] List<RoleDto> dto, CancellationToken token)
    {
        var result = await _roleCommands.InitializeRolesAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update a role", Description = "Updates an existing role using its ID and the new data.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Role updated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid role data")]
    public async Task<IActionResult> UpdateRole(Guid id, [FromBody] RoleDto dto, CancellationToken token)
    {
        var result = await _roleCommands.UpdateRoleAsync(id, dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet("activate/{id}/{versionId}")]
    [SwaggerOperation(Summary = "Activate a role", Description = "Activates a specific role using its ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Role activated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
    public async Task<IActionResult> ActivateUser(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _roleCommands.ActivateRoleAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet("deactivate/{id}/{versionId}")]
    [SwaggerOperation(Summary = "Deactivate a role", Description = "Deactivates a specific role using its ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Role deactivated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
    public async Task<IActionResult> DeactivateUser(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _roleCommands.DeactivateRoleAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpDelete("{id}/{versionId}")]
    [SwaggerOperation(Summary = "Delete a role", Description = "Deletes a specific role using its ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Role deleted successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
    public async Task<IActionResult> DeleteRole(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _roleCommands.DeactivateRoleAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }
}