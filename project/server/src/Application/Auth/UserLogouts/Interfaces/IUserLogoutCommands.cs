// source
using server.src.Domain.Dto.Common;

namespace server.src.Application.Auth.UserLogouts.Interfaces;

/// <summary>
/// Defines command operations related to user logouts.
/// </summary>
public interface IUserLogoutCommands
{
    /// <summary>
    /// Logs out a user based on their unique identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to be logged out.</param>
    /// <param name="token">A cancellation token to handle request cancellation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing a response with a status message indicating the logout result.</returns>
    Task<Response<string>> UserLogoutAsync(Guid userId, CancellationToken token = default);
}
