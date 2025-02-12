using System.Linq.Expressions;
using server.src.Domain.Auth.Users.Models;

namespace server.src.Application.Auth.Users.Projections;

public static class UserProjections
{
    public static Expression<Func<User, User>> GetUserProjection()
    {
        return u => new User
        { 
            Id = u.Id, 
        };
    }

    public static Expression<Func<User, User>> AssignUserProjection()
    {
        return u => new User
        { 
            Id = u.Id,
            IsActive = u.IsActive 
        };
    }
}