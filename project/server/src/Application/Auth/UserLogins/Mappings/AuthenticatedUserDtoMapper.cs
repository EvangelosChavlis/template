// source
using server.src.Domain.Auth.Users.Dtos;

namespace server.src.Application.Auth.UserLogins.Mappings;

/// <summary>
/// Provides mapping extensions for creating <see cref="AuthenticatedUserDto"/> instances.
/// </summary>
public static class AuthenticatedUserDtoMapper
{
    /// <summary>
    /// Maps a username and token to an <see cref="AuthenticatedUserDto"/>.
    /// </summary>
    /// <param name="userName">The username of the authenticated user.</param>
    /// <param name="token">The authentication token for the user.</param>
    /// <returns>An <see cref="AuthenticatedUserDto"/> containing the username and token.</returns>
    public static AuthenticatedUserDto AuthenticatedUserDtoMapping(this string userName, string token) 
        => new (UserName: userName, Token: token);
}
