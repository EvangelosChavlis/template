namespace server.src.Domain.Auth.Users.Dtos;

/// <summary>
/// Represents the data required to enable two-factor authentication for a user.
/// </summary>
/// <param name="UserId">The unique identifier of the user.</param>
/// <param name="VersionId">The version of the role for concurrency control during updates.</param>
public record Enable2FADto(Guid UserId, Guid VersionId);