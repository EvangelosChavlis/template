// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Auth.Users.Interfaces;
using server.src.Domain.Common.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Auth.Users.Dtos;

namespace server.src.Api.Controllers.Auth.Users;

[Route("api/auth/users")]
[ApiController]
public class UserQueriesController : BaseApiController 
{
    private readonly IUserQueries _userQueries;
    
    public UserQueriesController(IUserQueries userQueries)
    {
        _userQueries = userQueries;
    }

    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(Summary = "Retrieve all users", Description = "Fetches a list of all users.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully retrieved users", typeof(ListResponse<ListItemUserDto>))]
    public async Task<IActionResult> GetUsers([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _userQueries.GetUsersAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }

    
    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [Route("{id}")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(Summary = "Get user by ID", Description = "Fetches a user by their ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "User retrieved successfully", typeof(ListItemUserDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken token)
    {
        var result = await _userQueries.GetUserByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }

}