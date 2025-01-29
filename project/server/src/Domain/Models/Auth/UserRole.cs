namespace server.src.Domain.Models.Auth;

/// <summary>
/// Represents a custom relationship between a user and a role in the authentication system.
/// </summary>
public class UserRole
{
    /// <summary>
    /// Gets or sets the unique identifier for this relationship instance.
    /// This is used to uniquely identify the relationship between a user and a role.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the relationship was created.
    /// </summary>
    public DateTime Date { get; set; }

    #region Foreign Keys
    /// <summary>
    /// Gets or sets the unique identifier of the user associated with this role.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the role associated with the user.
    /// </summary>
    public Guid RoleId { get; set; }
    #endregion

    #region Navigation Properties
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
    #endregion
    
}