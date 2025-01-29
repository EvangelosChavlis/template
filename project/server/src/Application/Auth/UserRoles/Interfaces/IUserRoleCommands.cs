// source
using server.src.Domain.Dto.Common;

namespace server.src.Application.Auth.UserRoles.Interfaces;

public interface IUserRoleCommands
{
    /// <summary>
    /// Assigns a role to a user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="roleId">The unique identifier of the role to assign.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<Response<string>> AssignRoleToUserAsync(Guid userId, Guid roleId, 
        CancellationToken token = default);

    /// <summary>
    /// Unassigns a role from a user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="roleId">The unique identifier of the role to unassign.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<Response<string>> UnassignRoleFromUserAsync(Guid userId, Guid roleId, 
        CancellationToken token = default);
}