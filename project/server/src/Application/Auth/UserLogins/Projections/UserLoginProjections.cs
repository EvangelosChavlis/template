// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Auth.UserLogins.Models;

namespace server.src.Application.Auth.UserLogins.Projections;

public static class UserLoginProjections
{
    public static Expression<Func<UserLogin, UserLogin>> GetUserLoginsProjection()
    {
        return ul => new UserLogin
        {
            Id = ul.Id,
            LoginProvider = ul.LoginProvider,
            ProviderDisplayName = ul.ProviderDisplayName,
            Date = ul.Date
        };
    }
}