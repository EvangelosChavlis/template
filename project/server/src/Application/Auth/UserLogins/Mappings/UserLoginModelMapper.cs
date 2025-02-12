// source
using server.src.Domain.Auth.UserLogins.Models;
using server.src.Domain.Auth.Users.Models;

namespace server.src.Application.Auth.UserLogins.Mappings;

public static class UserLoginModelMapper
{
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