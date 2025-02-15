// source
using server.src.Domain.Auth.Permissions.Models;
using server.src.Domain.Auth.UserRoles.Models;
using server.src.Domain.Common.Models;

namespace server.src.Domain.Auth.Roles.Models;

/// <summary>
/// Represents a role in the authentication system.
/// </summary>
public class Role : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the role.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the normalized name of the role.
    /// This can be used for case-insensitive searches.
    /// </summary>
    public string NormalizedName { get; set; }

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

    #region Navigation properties

    /// <summary>
    /// Gets or sets the list of user roles associated with this role.
    /// Represents the many-to-many relationship between users and roles.
    /// </summary>
    public virtual List<UserRole> UserRoles { get; set; }

    public virtual List<Permission> Permissions { get; set; }

    #endregion
}
