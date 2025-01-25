// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;

namespace server.src.Application.Auth.Users.Interfaces;

/// <summary>
/// Defines methods for managing user-related actions, such as registration, updates, authentication, and account management.
/// </summary>
public interface IUserCommands
{
    /// <summary>
    /// Initializes a list of users in the system.
    /// </summary>
    /// <param name="dto">List of user data transfer objects.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the result of the operation.</returns>
    Task<Response<string>> InitializeUsersAsync(List<UserDto> dto, CancellationToken token = default);

    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="dto">User data transfer object containing user details.</param>
    /// <param name="registered">Indicates whether the user is already registered.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the result of the operation.</returns>
    Task<Response<string>> RegisterUserAsync(UserDto dto, bool registered, CancellationToken token = default);

    /// <summary>
    /// Updates an existing user's information.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="dto">User data transfer object containing updated user details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the result of the operation.</returns>
    Task<Response<string>> UpdateUserAsync(Guid id, UserDto dto, CancellationToken token = default);

    /// <summary>
    /// Initiates a forgot password process for the specified email.
    /// </summary>
    /// <param name="email">The user's email address.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the result of the operation.</returns>
    Task<Response<string>> ForgotPasswordAsync(string email, CancellationToken token = default);

    /// <summary>
    /// Resets a user's password using an authentication token.
    /// </summary>
    /// <param name="email">The user's email address.</param>
    /// <param name="authToken">The authentication token for password reset.</param>
    /// <param name="newPassword">The new password.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the result of the operation.</returns>
    Task<Response<string>> ResetPasswordAsync(string email, string authToken, string newPassword, CancellationToken token = default);

    /// <summary>
    /// Generates a new password for the specified user.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the result of the operation.</returns>
    Task<Response<string>> GeneratePasswordAsync(Guid id, CancellationToken token = default);

    /// <summary>
    /// Enables two-factor authentication (2FA) for a user.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the result of the operation.</returns>
    Task<Response<string>> Enable2FAAsync(Guid id, CancellationToken token = default);

    /// <summary>
    /// Verifies a user's two-factor authentication (2FA) code.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="authToken">The authentication token.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the result of the operation.</returns>
    Task<Response<string>> Verify2FAAsync(Guid id, string authToken, CancellationToken token = default);

    /// <summary>
    /// Refreshes an authentication token for a user.
    /// </summary>
    /// <param name="authToken">The authentication token to refresh.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the new authentication token.</returns>
    Task<Response<string>> RefreshTokenAsync(string authToken, CancellationToken token = default);

    /// <summary>
    /// Activates a user's account.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the result of the operation.</returns>
    Task<Response<string>> ActivateUserAsync(Guid id, CancellationToken token = default);

    /// <summary>
    /// Deactivates a user's account.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the result of the operation.</returns>
    Task<Response<string>> DeactivateUserAsync(Guid id, CancellationToken token = default);

    /// <summary>
    /// Locks a user's account, preventing access.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the result of the operation.</returns>
    Task<Response<string>> LockUserAsync(Guid id, CancellationToken token = default);

    /// <summary>
    /// Unlocks a previously locked user account.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the result of the operation.</returns>
    Task<Response<string>> UnlockUserAsync(Guid id, CancellationToken token = default);

    /// <summary>
    /// Confirms a user's email address.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the result of the operation.</returns>
    Task<Response<string>> ConfirmEmailUserAsync(Guid id, CancellationToken token = default);

    /// <summary>
    /// Confirms a user's phone number.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the result of the operation.</returns>
    Task<Response<string>> ConfirmPhoneNumberUserAsync(Guid id, CancellationToken token = default);

    /// <summary>
    /// Confirms a user's mobile phone number.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the result of the operation.</returns>
    Task<Response<string>> ConfirmMobilePhoneNumberUserAsync(Guid id, CancellationToken token = default);

    /// <summary>
    /// Deletes a user from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the result of the operation.</returns>
    Task<Response<string>> DeleteUserAsync(Guid id, CancellationToken token = default);
}
