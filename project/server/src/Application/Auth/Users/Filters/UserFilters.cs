// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Models.Auth;

namespace server.src.Application.Auth.Roles.Filters;

public static class UserFilters
{
    public static string UserNameSorting = typeof(User).GetProperty(nameof(User.UserName))!.Name;

    public static Expression<Func<User, bool>> UsersSearchFilter(this string filter)
    {
        return c => c.UserName!.Contains(filter ?? "") ||
            c.Email!.Contains(filter ?? "") || 
            c.FirstName.Contains(filter ?? "") ||
            c.LastName.Contains(filter ?? "") ||
            c.PhoneNumber!.Contains(filter ?? "") ||
            c.MobilePhoneNumber.Contains(filter ?? "");
    }

    public static Expression<Func<User, bool>>[] UserMatchFilters(this Expression<Func<User, bool>>? filter)
    {

        var filters = new Expression<Func<User, bool>>[]
        {
            filter!
        };

        return filters;
    }   
}