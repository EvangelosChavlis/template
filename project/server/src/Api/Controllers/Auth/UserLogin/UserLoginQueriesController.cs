// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Interfaces.Auth.UserLogins;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Common;
using server.src.WebApi.Controllers;

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
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a list of user logins", Description = "Retrieves a list of user login records associated with the given user ID, with optional query parameters for filtering and pagination.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of user login records retrieved successfully", typeof(ListResponse<List<ListItemUserLoginDto>>))]
    public async Task<IActionResult> GetGetLoginsByUserIdRoles(Guid id, [FromQuery] UrlQuery urlQuery, CancellationToken token)
        => Ok(await _userLoginQueries.GetLoginsByUserIdService(id, urlQuery, token));
}
