// packages
using Microsoft.AspNetCore.Identity;

namespace server.src.Domain.Models.Auth;

/// <summary>
/// Represents a relationship between a user and a role in the authentication system.
/// Inherits from IdentityUserRole to extend its functionality for a custom application.
/// </summary>
public class UserRole : IdentityUserRole<string>
{
    /// <summary>
    /// Gets or sets the user associated with the role.
    /// This establishes a many-to-many relationship between users and roles.
    /// </summary>
    public virtual User User { get; set; }

    /// <summary>
    /// Gets or sets the role associated with the user.
    /// This establishes a many-to-many relationship between users and roles.
    /// </summary>
    public virtual Role Role { get; set; }
}