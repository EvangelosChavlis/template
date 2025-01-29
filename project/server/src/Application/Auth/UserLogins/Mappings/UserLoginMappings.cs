// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Extensions;
using server.src.Domain.Models.Auth;

namespace server.src.Application.Auth.UserLogins.Mappings;

/// <summary>
/// Provides extension methods for mapping user data to user login records.
/// </summary>
public static class UserLoginMappings
{
    /// <summary>
    /// Maps a <see cref="UserLogin"/> model to a <see cref="ListItemUserLoginDto"/> DTO.
    /// </summary>
    /// <param name="model">The <see cref="UserLogin"/> model to map from.</param>
    /// <returns>A <see cref="ListItemUserLoginDto"/> containing mapped user login details.</returns>
    public static ListItemUserLoginDto ListItemUserLoginDtoMapping(this UserLogin model)
        => new (
            Id: model.Id,
            LoginProvider: model.LoginProvider!,
            ProviderDisplayName: model.ProviderDisplayName,
            Date: model.Date.GetFullLocalDateTimeString()
        );

    /// <summary>
    /// Maps a <see cref="UserLogin"/> model to a <see cref="ItemUserLoginDto"/> DTO.
    /// </summary>
    /// <param name="model">The <see cref="UserLogin"/> model to map from.</param>
    /// <returns>A <see cref="ItemUserLoginDto"/> containing mapped user login details.</returns>
    public static ItemUserLoginDto ItemUserLoginDtoMapping(this UserLogin model)
        => new (
            Id: model.Id,
            LoginProvider: model.LoginProvider,
            ProviderKey: model.ProviderKey,
            ProviderDisplayName: model.ProviderDisplayName,
            Date: model.Date.GetFullLocalDateTimeString(),
            User: model.User.UserName
        );

    /// <summary>
    /// Returns a default <see cref="ItemUserLoginDto"/> instance representing an error state.
    /// This is used when mapping a <see cref="UserLogin"/> model fails or returns invalid data.
    /// </summary>
    /// <returns>An <see cref="ItemUserLoginDto"/> with empty or default values.</returns>
    public static ItemUserLoginDto ErrorItemUserLoginDtoMapping()
        => new (
            Id: Guid.Empty,
            LoginProvider: string.Empty,
            ProviderKey: string.Empty,
            ProviderDisplayName: string.Empty,
            Date: string.Empty,
            User: string.Empty
        );


    /// <summary>
    /// Maps a <see cref="User"/> object to a new <see cref="UserLogin"/> object.
    /// </summary>
    /// <param name="user">The user to map from.</param>
    /// <returns>A new <see cref="UserLogin"/> object populated with data from the <paramref name="user"/>.</returns>
    public static UserLogin UserLoginMapping(this User user)
        => new ()
        {
            LoginProvider = user.UserName!,
            ProviderKey = Guid.NewGuid().ToString(),
            ProviderDisplayName = user.Email,
            Date = DateTime.UtcNow,
            UserId = user.Id,
            User = user
        };

    /// <summary>
    /// Maps a username and token to an AuthenticatedUserDto.
    /// </summary>
    /// <param name="userName">The username of the authenticated user.</param>
    /// <param name="token">The authentication token for the user.</param>
    /// <returns>An AuthenticatedUserDto containing the username and token.</returns>
    public static AuthenticatedUserDto AuthenticatedUserDtoMapping(this string userName, string token) 
        => new (UserName: userName, Token: token);
}
