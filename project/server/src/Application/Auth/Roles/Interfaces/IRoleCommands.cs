// source
using server.src.Domain.Auth.Roles.Dtos;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;

namespace server.src.Application.Auth.Roles.Interfaces;

/// <summary>
/// Defines methods for managing roles, including initialization, creation, updating, activation, deactivation, and deletion.
/// </summary>
public interface IRoleCommands
{
    /// <summary>
    /// Initializes a list of roles in the system.
    /// </summary>
    /// <param name="dto">A list of role data transfer objects to be initialized.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating the success or failure of the initialization process.</returns>
    Task<Response<string>> InitializeRolesAsync(List<CreateRoleDto> dto, 
        CancellationToken token = default);

    /// <summary>
    /// Creates a new role in the system.
    /// </summary>
    /// <param name="dto">The data transfer object containing the role details.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response containing the result message of the creation operation.</returns>
    Task<Response<string>> CreateRoleAsync(CreateRoleDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Updates an existing role in the system.
    /// </summary>
    /// <param name="id">The unique identifier of the role to update.</param>
    /// <param name="dto">The data transfer object containing the updated role details.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response containing the result message of the update operation.</returns>
    Task<Response<string>> UpdateRoleAsync(Guid id, UpdateRoleDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Activates a role by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the role to activate.</param>
    /// <param name="version">The version of the role for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the activation was successful.</returns>
    Task<Response<string>> ActivateRoleAsync(Guid id,
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deactivates a role by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the role to deactivate.</param>
    /// <param name="version">The version of the role for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the deactivation was successful.</returns>
    Task<Response<string>> DeactivateRoleAsync(Guid id,
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deletes a role from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the role to delete.</param>
    /// <param name="version">The version of the role for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response containing the result message of the deletion operation.</returns>
    Task<Response<string>> DeleteRoleAsync(Guid id, 
        Guid version, CancellationToken token = default);
}
