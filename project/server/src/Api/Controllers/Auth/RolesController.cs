// packages
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Interfaces.Auth;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;

namespace server.src.WebApi.Controllers.Auth;

[Route("api/auth/roles")]
[ApiController]
public class RolesController : BaseApiController
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [SwaggerOperation(Summary = "Retrieve all roles", Description = "Fetches a list of all available roles.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully retrieved roles", typeof(List<ItemRoleDto>))]
    public async Task<IActionResult> GetRoles()
        => Ok(await _roleService.GetRolesService());


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Retrieve a role by ID", Description = "Fetches a specific role using its ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully retrieved role", typeof(ItemResponse<ItemRoleDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
    public async Task<IActionResult> GetRoleById(string id)
        => Ok(await _roleService.GetRoleByIdService(id));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new role", Description = "Creates a new role using the provided role data.")]
    [SwaggerResponse(StatusCodes.Status201Created, "Role created successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid role data")]
    public async Task<IActionResult> CreateRole([FromBody] RoleDto dto)
        => Ok(await _roleService.CreateRoleService(dto));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpPost("initialize")]
    [SwaggerOperation(Summary = "Initialize roles", Description = "Initializes multiple roles using a list of role data.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Roles initialized successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid roles data")]
    public async Task<IActionResult> InitializeRole([FromBody] List<RoleDto> dto)
        => Ok(await _roleService.InitializeRolesService(dto));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update a role", Description = "Updates an existing role using its ID and the new data.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Role updated successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid role data")]
    public async Task<IActionResult> UpdateRole(string id, [FromBody] RoleDto dto)
        => Ok(await _roleService.UpdateRoleService(id, dto));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet("activate/{id}")]
    [SwaggerOperation(Summary = "Activate a role", Description = "Activates a specific role using its ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Role activated successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
    public async Task<IActionResult> ActivateUser(string id)
        => Ok(await _roleService.ActivateRoleService(id));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet("deactivate/{id}")]
    [SwaggerOperation(Summary = "Deactivate a role", Description = "Deactivates a specific role using its ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Role deactivated successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
    public async Task<IActionResult> DeactivateUser(string id)
        => Ok(await _roleService.DeactivateRoleService(id));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete a role", Description = "Deletes a specific role using its ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Role deleted successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
    public async Task<IActionResult> DeleteRole(string id)
        => Ok(await _roleService.DeleteRoleService(id));
}
