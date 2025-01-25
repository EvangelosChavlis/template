// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.WebApi.Controllers;
using server.src.Domain.Models.Common;
using server.src.Application.Auth.Roles.Interfaces;

namespace server.src.Api.Controllers.Auth.Roles;

[Route("api/auth/roles")]
[ApiController]
[Authorize(Roles = "Administrator")]
public class RoleQueriesController : BaseApiController
{
    private readonly IRoleQueries _roleQueries;
    
    public RoleQueriesController(IRoleQueries roleQueries)
    {
        _roleQueries = roleQueries;
    }

    
    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of roles", Description = "Retrieves a list of roles with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of roles retrieved successfully", typeof(ListResponse<List<ItemRoleDto>>))]
    public async Task<IActionResult> GetRoles([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _roleQueries.GetRolesAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet("user/{id}")]
    [SwaggerOperation(Summary = "Get a list of roles by user id", Description = "Retrieves a list of roles by user id with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of roles by user id retrieved successfully", typeof(ListResponse<List<ItemRoleDto>>))]
    public async Task<IActionResult> GetRolesByUserId(Guid id, [FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _roleQueries.GetRolesByUserIdAsync(id, urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Retrieve a role by ID", Description = "Fetches a specific role using its ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully retrieved role", typeof(Response<ItemRoleDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
    public async Task<IActionResult> GetRoleById(Guid id, CancellationToken token)
    {
        var result = await _roleQueries.GetRoleByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }

    
    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [Route("picker")]
    [SwaggerOperation(Summary = "Get a list of weather roles for the picker", Description = "Retrieves a list of roles available for selection in the picker.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of roles for picker retrieved successfully", typeof(Response<List<PickerRoleDto>>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No roles found for picker")]
    public async Task<IActionResult> GetRolesPicker(CancellationToken token)
    {
        var result = await _roleQueries.GetRolesPickerAsync(token);
        return StatusCode(result.StatusCode, result);
    }
}