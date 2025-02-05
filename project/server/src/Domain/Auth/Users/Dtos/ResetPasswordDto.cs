namespace server.src.Domain.Auth.Users.Dtos;

/// <summary>
/// Represents the data required to reset a user's password.
/// </summary>
/// <param name="Email">The user's email address.</param>
/// <param name="Token">The token required to authorize the password reset.</param>
/// <param name="NewPassword">The new password for the user.</param>
/// <param name="VersionId">The version of the role for concurrency control during updates.</param>
public record ResetPasswordDto(string Email, string Token, 
    string NewPassword, Guid VersionId);