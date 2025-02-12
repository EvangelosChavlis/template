// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Auth.UserLogouts.Models;

namespace server.src.Application.Auth.UserLogouts.Projection;

public static class UserLogoutProjections
{
    public static Expression<Func<UserLogout, UserLogout>> GetUserLogoutsProjection()
    {
        return ul => new UserLogout
        {
            Id = ul.Id,
            LoginProvider = ul.LoginProvider,
            ProviderDisplayName = ul.ProviderDisplayName,
            Date = ul.Date
        };
    }
}