// source
using server.src.Domain.Auth.Roles.Models;
using server.src.Domain.Common.Models;

namespace server.src.Application.Auth.Roles.Includes;

public class RoleIncludes
{
    public static IncludeThenInclude<Role>[] GetRolesIncludes()
    {
        return
        [
            
        ];
    }

    public static IncludeThenInclude<Role>[] GetRolesByUserIdIncludes()
    {
        return
        [
            new (v => v.UserRoles)
        ];
    }
}