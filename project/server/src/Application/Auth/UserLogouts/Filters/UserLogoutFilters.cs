// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Auth.UserLogouts.Models;

namespace server.src.Application.Auth.UserLogouts.Filters;

public static class UserLogoutFilters
{
    public static string UserLogoutDateSorting = typeof(UserLogout).GetProperty(nameof(UserLogout.Date))!.Name;

    public static Expression<Func<UserLogout, bool>> UserLogoutSearchFilter(this string filter)
    {
        return ul => ul.Date.ToString().Contains(filter ?? "") ||
            ul.ProviderDisplayName.Contains(filter ?? "");
    }

    public static Expression<Func<UserLogout, bool>>[] UserLogoutMatchFilters(
        this Expression<Func<UserLogout, bool>>? filter, Guid userId)
    {
        var filters = new Expression<Func<UserLogout, bool>>[]
        {
            filter!,
            ul => ul.UserId == userId
        };

        return filters;
    }
}