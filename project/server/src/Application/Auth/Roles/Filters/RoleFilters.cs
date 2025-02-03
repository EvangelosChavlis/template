// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Auth.Roles.Models;

namespace server.src.Application.Auth.Roles.Filters;

public static class RoleFilters
{
    public static string RoleNameSorting = typeof(Role).GetProperty(nameof(Role.Name))!.Name;

    public static Expression<Func<Role, bool>> RoleSearchFilter(this string filter)
    {
        return o => o.Name.Contains(filter ?? "") ||
            o.Description.Contains(filter ?? "");
    }

    public static Expression<Func<Role, bool>>[] RoleMatchFilters(this Expression<Func<Role, bool>>? filter, Guid userId)
    {
        var filters = new Expression<Func<Role, bool>>[]
        {
            filter!,
            x => x.UserRoles.Any(ur => ur.UserId == userId)
        };

        return filters;
    }
}