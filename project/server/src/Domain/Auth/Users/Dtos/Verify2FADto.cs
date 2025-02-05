namespace server.src.Domain.Auth.Users.Dtos;

/// <summary>
/// Represents the data required to verify two-factor authentication for a user.
/// </summary>
/// <param name="UserId">The unique identifier of the user.</param>
/// <param name="Token">The verification token for two-factor authentication.</param>
/// <param name="VersionId">The version of the role for concurrency control during updates.</param>
public record Verify2FADto(Guid UserId, string Token, Guid VersionId);