namespace server.src.Domain.Auth.Users.Dtos;

/// <summary>
/// Represents the credentials required for a user login request.
/// </summary>
/// <param name="Username">The username of the user.</param>
/// <param name="Password">The password of the user.</param>
public record UserLoginDto(string Username, string Password);