// source
using server.src.Domain.Auth.UserLogouts.Dtos;
using server.src.Domain.Auth.UserLogouts.Dtos;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;

namespace server.src.Application.Auth.UserLogouts.Interfaces;

/// <summary>
/// Defines query operations related to user logout records.
/// </summary>
public interface IUserLogoutQueries
{
    /// <summary>
    /// Retrieves a paginated list of user logout records based on the specified user ID.  
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="pageParams">The pagination and filtering parameters.</param>
    /// <param name="token">A cancellation token for async operation control (optional).</param>
    /// <returns>A task that resolves to a paginated list of <see cref="ListItemUserLogoutDto"/> records.</returns>
    Task<ListResponse<List<ListItemUserLogoutDto>>> GetLogoutsByUserIdAsync(Guid id, 
        UrlQuery pageParams, CancellationToken token = default);

    /// <summary>
    /// Retrieves a specific user login record by its unique identifier.  
    /// </summary>
    /// <param name="id">The unique identifier of the user login record.</param>
    /// <param name="token">A cancellation token for async operation control (optional).</param>
    /// <returns>
    /// A task that resolves to a <see cref="ItemUserLogoutDto"/>  
    /// containing the user login details.
    /// </returns>
    Task<Response<ItemUserLogoutDto>> GetUserLogoutByIdAsync(Guid id, 
        CancellationToken token = default);
}
