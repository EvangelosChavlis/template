// source
using server.src.Domain.Auth.UserLogouts.Models;
using server.src.Domain.Auth.Users.Models;

namespace server.src.Application.Auth.UserLogouts.Mappings;

/// <summary>
/// Provides mapping extensions for converting a <see cref="User"/> model to a <see cref="UserLogout"/> model.
/// </summary>
public static class UserLogoutModelMapper
{
    /// <summary>
    /// Maps a <see cref="User"/> object to a new <see cref="UserLogout"/> object.
    /// </summary>
    /// <param name="user">The user to map from.</param>
    /// <returns>A new <see cref="UserLogout"/> object populated with data from the <paramref name="user"/>.</returns>
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
