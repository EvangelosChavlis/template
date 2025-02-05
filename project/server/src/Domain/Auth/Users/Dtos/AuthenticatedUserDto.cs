namespace server.src.Domain.Auth.Users.Dtos;

/// <summary>
/// Represents an authenticated user with their username and access token.
/// </summary>
/// <param name="UserName">The username of the authenticated user.</param>
/// <param name="Token">The access token for the authenticated session.</param>
public record AuthenticatedUserDto(string UserName, string Token);