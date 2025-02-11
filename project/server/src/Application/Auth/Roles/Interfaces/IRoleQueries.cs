// source
using server.src.Domain.Auth.Roles.Dtos;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;

namespace server.src.Application.Auth.Roles.Interfaces;

/// <summary>
/// Defines methods for retrieving role-related data, including role lists, role details, and role selection options.
/// </summary>
public interface IRoleQueries
{
    /// <summary>
    /// Retrieves a paginated list of roles based on query parameters.
    /// </summary>
    /// <param name="urlQuery">Query parameters for filtering and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A list response containing roles.</returns>
    Task<ListResponse<List<ListItemRoleDto>>> GetRolesAsync(UrlQuery urlQuery, 
        CancellationToken token = default);

    /// <summary>
    /// Retrieves a paginated list of roles assigned to a specific user.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="urlQuery">Query parameters for filtering and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A list response containing roles assigned to the specified user.</returns>
    Task<ListResponse<List<ItemRoleDto>>> GetRolesByUserIdAsync(Guid id, UrlQuery urlQuery, 
        CancellationToken token = default);

    /// <summary>
    /// Retrieves a list of roles for selection purposes.
    /// </summary>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing a list of role picker items.</returns>
    Task<Response<List<PickerRoleDto>>> GetRolesPickerAsync(CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed information about a specific role by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the role.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing detailed role information.</returns>
    Task<Response<ItemRoleDto>> GetRoleByIdAsync(Guid id, 
        CancellationToken token = default);
}
