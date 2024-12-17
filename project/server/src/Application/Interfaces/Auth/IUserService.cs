// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Common;

namespace server.src.Application.Interfaces.Auth;

/// <summary>
/// Interface for managing user-related operations and authentication functionalities.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Retrieves a paginated list of users.
    /// </summary>
    /// <param name="pageParams">Query parameters for pagination and filtering.</param>
    /// <param name="token">Cancellation token for the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list response of users.</returns>
    Task<ListResponse<List<ListItemUserDto>>> GetUsersService(UrlQuery pageParams, CancellationToken token = default);

    /// <summary>
    /// Retrieves the details of a specific user by ID.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="token">Cancellation token for the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user's details wrapped in an ItemResponse.</returns>
    Task<ItemResponse<ItemUserDto>> GetUserByIdService(string id, CancellationToken token = default);

    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="dto">The details of the user to register.</param>
    /// <param name="registered">Indicates whether the user is already registered.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with the status or user ID.</returns>
    Task<CommandResponse<string>> RegisterUserService(UserDto dto, bool registered);

    /// <summary>
    /// Initializes multiple users in the system.
    /// </summary>
    /// <param name="dto">A list of users to initialize.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> InitializeUsersService(List<UserDto> dto);

    /// <summary>
    /// Logs in a user using the provided credentials.
    /// </summary>
    /// <param name="dto">The login credentials of the user.</param>
    /// <returns>A task that represents the asynchronous operation. 
    /// The task result contains a CommandResponse with the authenticated user's details.</returns>
    Task<CommandResponse<AuthenticatedUserDto>> LoginUserService(UserLoginDto dto);

    /// <summary>
    /// Logs out a user by their ID.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to log out.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> LogoutUserService(string userId);

    /// <summary>
    /// Refreshes a user's authentication token.
    /// </summary>
    /// <param name="token">The current token to refresh.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with the new token.</returns>
    Task<CommandResponse<string>> RefreshTokenService(string token);

    /// <summary>
    /// Initiates the password recovery process for a user.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> ForgotPasswordService(string email);

    /// <summary>
    /// Resets the user's password using a token.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <param name="token">The password reset token.</param>
    /// <param name="newPassword">The new password.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> ResetPasswordService(string email, string token, string newPassword);

    /// <summary>
    /// Generates a new password for a user.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> GeneratePasswordService(string id);

    /// <summary>
    /// Enables two-factor authentication (2FA) for a user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> Enable2FAService(string userId);

    /// <summary>
    /// Verifies a user's two-factor authentication (2FA) token.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="token">The 2FA token to verify.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> Verify2FAService(string userId, string token);

    /// <summary>
    /// Updates the details of an existing user.
    /// </summary>
    /// <param name="id">The unique identifier of the user to update.</param>
    /// <param name="dto">The updated user details.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> UpdateUserService(string id, UserDto dto);

    /// <summary>
    /// Activates a user account, making it available for use.
    /// </summary>
    /// <param name="id">The unique identifier of the user to activate.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> ActivateUserService(string id);

    /// <summary>
    /// Deactivates a user account, making it unavailable for use.
    /// </summary>
    /// <param name="id">The unique identifier of the user to deactivate.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> DeactivateUserService(string id);

    /// <summary>
    /// Locks a user account, restricting access to it.
    /// </summary>
    /// <param name="id">The unique identifier of the user to lock.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> LockUserService(string id);

    /// <summary>
    /// Unlocks a previously locked user account.
    /// </summary>
    /// <param name="id">The unique identifier of the user to unlock.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> UnlockUserService(string id);

    /// <summary>
    /// Confirms the email address associated with a user account.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> ConfirmEmailUserService(string id);

    /// <summary>
    /// Confirms the phone number associated with a user account.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> ConfirmPhoneNumberUserService(string id);

    /// <summary>
    /// Confirms the mobile phone number associated with a user account.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> ConfirmMobilePhoneNumberUserService(string id);

    /// <summary>
    /// Deletes a user from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> DeleteUserService(string id);

    /// <summary>
    /// Assigns a role to a user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="roleId">The unique identifier of the role to assign.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> AssignRoleToUserService(string userId, string roleId);
}
