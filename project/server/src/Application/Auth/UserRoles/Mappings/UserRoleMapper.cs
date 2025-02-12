// source
using server.src.Domain.Auth.Roles.Models;
using server.src.Domain.Auth.UserRoles.Models;
using server.src.Domain.Auth.Users.Models;

namespace server.src.Application.Auth.UserRoles.Mappings;

/// <summary>
/// Provides mappings for the relationship between users and roles.
/// </summary>
public static class UserRoleMapper
{
    /// <summary>
    /// Maps the role and user models to a user-role relationship model.
    /// This method assigns a role to a user through the user-role mapping model.
    /// </summary>
    /// <param name="roleModel">The role model representing the role being assigned.</param>
    /// <param name="userModel">The user model representing the user who will receive the role.</param>
    /// <param name="userRoleModel">The user-role model to be populated with the role and user information.</param>
    public static UserRole AssignRoleToUserMapping(this Role roleModel, User userModel)
    => new ()
    {
        Date = DateTime.UtcNow,
        User = userModel,
        UserId = userModel.Id,
        Role = roleModel,
        RoleId = roleModel.Id
    };
}