using System.Linq.Expressions;
using server.src.Domain.Auth.Roles.Models;

namespace server.src.Application.Auth.Roles.Projections;

public static class RoleProjections
{
    public static Expression<Func<Role, Role>> GetRoleProjection()
    {
        return r => new Role
        {
            Id = r.Id,
            Name = r.Name,
            Description = r.Description,
            IsActive = r.IsActive
        };
    }
}