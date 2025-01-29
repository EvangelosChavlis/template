// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Common;

namespace server.src.Application.Auth.UserLogins.Interfaces;

/// <summary>
/// Provides query operations for retrieving user login records.  
/// This interface defines methods to fetch login details based on user ID  
/// and retrieve specific login records.
/// </summary>
public interface IUserLoginQueries
{
    /// <summary>
    /// Retrieves a paginated list of user login records for a given user.  
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="urlQuery">Pagination and filtering parameters.</param>
    /// <param name="token">A cancellation token for async operation control (optional).</param>
    /// <returns>
    /// A task that resolves to a paginated list of <see cref="ListItemUserLoginDto"/> records.  
    /// </returns>
    Task<ListResponse<List<ListItemUserLoginDto>>> GetLoginsByUserIdAsync(Guid id, 
        UrlQuery urlQuery, CancellationToken token = default);

    /// <summary>
    /// Retrieves a specific user login record by its unique identifier.  
    /// </summary>
    /// <param name="id">The unique identifier of the user login record.</param>
    /// <param name="token">A cancellation token for async operation control (optional).</param>
    /// <returns>
    /// A task that resolves to a <see cref="Response{ItemUserLoginDto}"/>  
    /// containing the user login details.
    /// </returns>
    Task<Response<ItemUserLoginDto>> GetUserLoginByIdAsync(Guid id, 
        CancellationToken token = default);
}