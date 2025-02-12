// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Auth.Roles.Models;

namespace server.src.Application.Auth.Roles.Projections;

public static class RoleProjections
{
    public static Expression<Func<Role, Role>> GetRolesProjection()
    {
        return r => new Role
        {
            Id = r.Id,
            Name = r.Name,
            Description = r.Description,
            IsActive = r.IsActive
        };
    }

    public static Expression<Func<Role, Role>> GetRolesPickerProjection()
    {
        return r => new Role
        {
            Id = r.Id,
            Name = r.Name,
            IsActive = r.IsActive
        };
    }


    public static Expression<Func<Role, Role>> GetRoleByIdProjection()
    {
        return r => new Role
        { 
            Id = r.Id, 
            Name = r.Name,
            Description = r.Description,
            IsActive = r.IsActive,
            Version = r.Version 
        };
    }

    public static Expression<Func<Role, Role>> AssignRoleProjection()
    {
        return r => new Role
        { 
            Id = r.Id,
            IsActive = r.IsActive 
        };
    }
}