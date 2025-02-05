namespace server.src.Domain.Auth.Users.Dtos;

/// <summary>
/// Represents the data required to request a password reset.
/// </summary>
/// <param name="Email">The email address of the user requesting a password reset.</param>
/// <param name="VersionId">The version of the role for concurrency control during updates.</param>
public record ForgotPasswordDto(string Email, Guid VersionId);