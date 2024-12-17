// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;

namespace server.src.Application.Interfaces.Auth;

/// <summary>
/// Interface for managing role-related operations in the system.
/// </summary>
public interface IRoleService
{
    /// <summary>
    /// Retrieves a list of all roles.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of roles.</returns>
    Task<List<ItemRoleDto>> GetRolesService();

    /// <summary>
    /// Retrieves the details of a specific role by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the role.</param>
    /// <returns>A task that represents the asynchronous operation. 
    /// The task result contains the role details wrapped in an ItemResponse.</returns>
    Task<ItemResponse<ItemRoleDto>> GetRoleByIdService(string id);

    /// <summary>
    /// Creates a new role in the system.
    /// </summary>
    /// <param name="dto">The details of the role to create.</param>
    /// <returns>A task that represents the asynchronous operation. 
    /// The task result contains a CommandResponse with the ID of the newly created role.</returns>
    Task<CommandResponse<string>> CreateRoleService(RoleDto dto);

    /// <summary>
    /// Initializes multiple roles in the system.
    /// </summary>
    /// <param name="dto">A list of roles to initialize.</param>
    /// <returns>A task that represents the asynchronous operation. 
    /// The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> InitializeRolesService(List<RoleDto> dto);

    /// <summary>
    /// Updates the details of an existing role.
    /// </summary>
    /// <param name="id">The unique identifier of the role to update.</param>
    /// <param name="dto">The updated role details.</param>
    /// <returns>A task that represents the asynchronous operation. 
    /// The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> UpdateRoleService(string id, RoleDto dto);

    /// <summary>
    /// Activates a role, making it available for use in the system.
    /// </summary>
    /// <param name="id">The unique identifier of the role to activate.</param>
    /// <returns>A task that represents the asynchronous operation. 
    /// The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> ActivateRoleService(string id);

    /// <summary>
    /// Deactivates a role, making it unavailable for use in the system.
    /// </summary>
    /// <param name="id">The unique identifier of the role to deactivate.</param>
    /// <returns>A task that represents the asynchronous operation. 
    /// The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> DeactivateRoleService(string id);

    /// <summary>
    /// Deletes a role from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the role to delete.</param>
    /// <returns>A task that represents the asynchronous operation. 
    /// The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> DeleteRoleService(string id);
}
