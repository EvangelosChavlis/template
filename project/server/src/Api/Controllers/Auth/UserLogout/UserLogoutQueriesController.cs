// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Common;
using server.src.WebApi.Controllers;
using server.src.Application.Auth.UserLogouts.Interfaces;

namespace server.src.Api.Controllers.Auth.UserLogin;

[Route("api/auth/userLogins")]
[ApiController]
[Authorize(Roles = "Administrator")]
public class UserLogoutQueriesController : BaseApiController
{
    private readonly IUserLogoutQueries _userLogoutQueries;
    
    public UserLogoutQueriesController(IUserLogoutQueries userLogoutQueries)
    {
        _userLogoutQueries = userLogoutQueries;
    }

    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [Route("user/{id}")]
    [SwaggerOperation(Summary = "Get a list of user logouts", Description = "Retrieves a list of user logout records associated with the given user ID, with optional query parameters for filtering and pagination.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of user logout records retrieved successfully", typeof(ListResponse<List<ListItemUserLogoutDto>>))]
    public async Task<IActionResult> GetGetLogoutsByUserIdRoles(Guid id, [FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _userLogoutQueries.GetLogoutsByUserIdAsync(id, urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }

    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get user logout by ID", Description = "Retrieves details of a specific user logout record based on its unique identifier.")]
    [SwaggerResponse(StatusCodes.Status200OK, "User logout record retrieved successfully.", typeof(Response<ItemUserLogoutDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User logout record not found.")]
    public async Task<IActionResult> GetUserLogoutById(Guid id, CancellationToken token)
    {
        var result = await _userLogoutQueries.GetUserLogoutByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }
}
