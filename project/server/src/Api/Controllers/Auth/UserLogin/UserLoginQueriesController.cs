// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Common;
using server.src.WebApi.Controllers;
using server.src.Application.Auth.UserLogins.Interfaces;

namespace server.src.Api.Controllers.Auth.UserLogin;

[Route("api/auth/userLogins")]
[ApiController]
[Authorize(Roles = "Administrator")]
public class UserLoginQueriesController : BaseApiController
{
    private readonly IUserLoginQueries _userLoginQueries;
    
    public UserLoginQueriesController(IUserLoginQueries userLoginQueries)
    {
        _userLoginQueries = userLoginQueries;
    }

    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [Route("user/{id}")]
    [SwaggerOperation(Summary = "Get a list of user logins", Description = "Retrieves a list of user login records associated with the given user ID, with optional query parameters for filtering and pagination.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of user login records retrieved successfully", typeof(ListResponse<List<ListItemUserLoginDto>>))]
    public async Task<IActionResult> GetLoginsByUserId(Guid id, [FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _userLoginQueries.GetLoginsByUserIdAsync(id, urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }

    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get user login by ID", Description = "Retrieves details of a specific user login record based on its unique identifier.")]
    [SwaggerResponse(StatusCodes.Status200OK, "User login record retrieved successfully.", typeof(Response<ItemUserLoginDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User login record not found.")]
    public async Task<IActionResult> GetUserLoginById(Guid id, CancellationToken token)
    {
        var result = await _userLoginQueries.GetUserLoginByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }
}
