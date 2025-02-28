// source
using server.src.Domain.Auth.Users.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Application.Auth.UserLogins.Interfaces;

/// <summary>
/// Defines command operations related to user logins, such as authentication.
/// </summary>
public interface IUserLoginCommands
{
    /// <summary>
    /// Authenticates a user using the provided login credentials.
    /// </summary>
    /// <param name="dto">The login credentials, including username and password.</param>
    /// <param name="token">A cancellation token to handle request cancellation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing a response with authenticated user details.</returns>
    Task<Response<AuthenticatedUserDto>> UserLoginAsync(UserLoginDto dto, CancellationToken token = default);
}
