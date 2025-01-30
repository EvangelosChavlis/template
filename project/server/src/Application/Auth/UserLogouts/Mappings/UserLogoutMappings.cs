// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Extensions;
using server.src.Domain.Models.Auth;

namespace server.src.Application.Auth.UserLogouts.Mappings;

/// <summary>
/// Provides extension methods for mapping user data to user logout records.
/// </summary>
public static class UserLogoutMappings
{
    /// <summary>
    /// Maps a <see cref="UserLogout"/> model to a <see cref="ItemUserLogoutDto"/> DTO.
    /// </summary>
    /// <param name="model">The <see cref="UserLogout"/> model to map from.</param>
    /// <returns>A <see cref="ItemUserLogoutDto"/> containing mapped user login details.</returns>
    public static ItemUserLogoutDto ItemUserLogoutDtoMapping(this UserLogout model)
        => new (
            Id: model.Id,
            LoginProvider: model.LoginProvider,
            ProviderKey: model.ProviderKey,
            ProviderDisplayName: model.ProviderDisplayName,
            Date: model.Date.GetFullLocalDateTimeString(),
            User: model.User.UserName
        );

    /// <summary>
    /// Returns a default <see cref="ItemUserLogoutDto"/> instance representing an error state.
    /// This is used when mapping a <see cref="UserLogout"/> model fails or returns invalid data.
    /// </summary>
    /// <returns>An <see cref="ItemUserLogoutDto"/> with empty or default values.</returns>
    public static ItemUserLogoutDto ErrorItemUserLogoutDtoMapping()
        => new (
            Id: Guid.Empty,
            LoginProvider: string.Empty,
            ProviderKey: string.Empty,
            ProviderDisplayName: string.Empty,
            Date: string.Empty,
            User: string.Empty
        );

    /// <summary>
    /// Maps a <see cref="UserLogout"/> model to a <see cref="ListItemUserLogoutDto"/> DTO.
    /// </summary>
    /// <param name="model">The <see cref="UserLogin"/> model to map from.</param>
    /// <returns>A <see cref="ListItemUserLogoutDto"/> containing mapped user login details.</returns>
    public static ListItemUserLogoutDto ListItemUserLogoutDtoMapping(this UserLogout model)
        => new (
            Id: model.Id,
            LoginProvider: model.LoginProvider!,
            ProviderDisplayName: model.ProviderDisplayName,
            Date: model.Date.GetFullLocalDateTimeString()
        );

    /// <summary>
    /// Maps a <see cref="User"/> object to a new <see cref="UserLogout"/> object.
    /// </summary>
    /// <param name="user">The user to map from.</param>
    /// <returns>A new <see cref="UserLogin"/> object populated with data from the <paramref name="user"/>.</returns>
    public static UserLogout UserLogoutMapping(this User user)
        => new ()
        {
            LoginProvider = user.UserName!,
            ProviderKey = Guid.NewGuid().ToString(),
            ProviderDisplayName = user.Email,
            Date = DateTime.UtcNow,
            UserId = user.Id,
            User = user
        };
}
