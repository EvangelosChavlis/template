// packages
using Microsoft.AspNetCore.Identity;

namespace server.src.Domain.Models.Auth;

/// <summary>
/// Represents a role in the authentication system, extending the IdentityRole class to include additional properties.
/// </summary>
public class Role : IdentityRole
{
    /// <summary>
    /// Gets or sets the description of the role.
    /// Provides additional details about the role's purpose or functionality.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the status indicating if the role is active.
    /// Determines whether the role is enabled for assignment or usage.
    /// </summary>
    public bool IsActive { get; set; }

    #region Navigation Properties

    /// <summary>
    /// Gets or sets the list of user roles associated with this role.
    /// Represents the many-to-many relationship between users and roles.
    /// </summary>
    public List<UserRole> UserRoles { get; set; }

    #endregion
}
