namespace server.src.Domain.Models.Auth;

/// <summary>
/// Represents a claim associated with a user in the system.
/// </summary>
public class UserClaim
{
    /// <summary>
    /// Gets or sets the identifier for this user claim.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the claim type for this claim.
    /// Example: "Role", "Permission", or any other claim type.
    /// </summary>
    public string ClaimType { get; set; }

    /// <summary>
    /// Gets or sets the claim value for this claim.
    /// Example: "Admin", "ReadAccess", etc.
    /// </summary>
    public string ClaimValue { get; set; }

    #region Foreign Keys
    /// <summary>
    /// Gets or sets the primary key of the user associated with this claim.
    /// This serves as a foreign key to the User entity.
    /// </summary>
    public Guid UserId { get; set; }
    #endregion

    #region Navigation Properties
    /// <summary>
    /// Gets or sets the associated user for this claim.
    /// Represents the many-to-one relationship between claims and users.
    /// </summary>
    public User User { get; set; }
    #endregion
}
