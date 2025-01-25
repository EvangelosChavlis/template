// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Common;

namespace server.src.Application.Interfaces.Auth.UserLogins;

/// <summary>
/// Defines query operations related to user login records.
/// </summary>
public interface IUserLoginQueries
{
    /// <summary>
    /// Retrieves a paginated list of user login records based on the specified user ID.  
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="pageParams">The pagination and filtering parameters.</param>
    /// <param name="token">A cancellation token for async operation control (optional).</param>
    /// <returns>A task that resolves to a paginated list of <see cref="ListItemUserLoginDto"/> records.</returns>
    Task<ListResponse<List<ListItemUserLoginDto>>> GetLoginsByUserIdService(
        Guid id, 
        UrlQuery pageParams, 
        CancellationToken token = default
    );
}
