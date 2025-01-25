// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Extensions;
using server.src.Domain.Models.Auth;

namespace server.src.Application.Mappings.Auth;

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
}
