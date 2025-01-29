// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Models.Auth;

namespace server.src.Application.Auth.UserLogins.Filters;

public static class UserLoginFilters
{
    public static string UserLoginDateSorting = typeof(UserLogin).GetProperty(nameof(UserLogin.Date))!.Name;

    public static Expression<Func<UserLogin, bool>> UserLoginSearchFilter(this string filter)
    {
        return ul => ul.Date.ToString().Contains(filter ?? "") ||
            ul.ProviderDisplayName.Contains(filter ?? "");
    }

    public static Expression<Func<UserLogin, bool>>[] UserLoginMatchFilters(this Expression<Func<UserLogin, bool>>? filter)
    {
        var filters = new Expression<Func<UserLogin, bool>>[]
        {
            filter!
        };

        return filters;
    }
}