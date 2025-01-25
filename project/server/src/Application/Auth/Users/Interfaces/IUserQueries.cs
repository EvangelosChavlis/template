// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Common;

namespace server.src.Application.Auth.Users.Interfaces;

/// <summary>
/// Defines methods for retrieving user-related data, including listing users and fetching user details by ID.
/// </summary>
public interface IUserQueries
{
    /// <summary>
    /// Retrieves a paginated list of users based on the provided query parameters.
    /// </summary>
    /// <param name="urlQuery">The query parameters for filtering and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing a list of users.</returns>
    Task<ListResponse<List<ListItemUserDto>>> GetUsersAsync(UrlQuery urlQuery, 
        CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed information about a specific user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the user's details.</returns>
    Task<Response<ItemUserDto>> GetUserByIdAsync(Guid id, 
        CancellationToken token = default);
}
