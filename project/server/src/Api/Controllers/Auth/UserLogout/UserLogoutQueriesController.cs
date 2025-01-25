// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Interfaces.Auth.UserLogouts;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Common;
using server.src.WebApi.Controllers;

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
        => Ok(await _userLogoutQueries.GetLogoutsByUserIdService(id, urlQuery, token));
}
