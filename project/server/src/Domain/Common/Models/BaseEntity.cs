using server.src.Domain.Auth.Users.Models;

namespace server.src.Domain.Common.Models;

/// <summary>
/// Base class for entities that provides common fields like Id, Version, LockUntil, and TenantId.
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the base entity.
    /// This serves as the primary key for entities that inherit from this class.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the version GUID for optimistic concurrency control.
    /// This ensures that only one user can modify an entity at a time, preventing concurrency issues.
    /// </summary>
    public Guid Version { get; set; }

    /// <summary>
    /// Gets or sets the expiration timestamp for the entity lock.
    /// If the value is null or in the past, the entity is considered unlocked.
    /// </summary>
    public DateTime? LockUntil { get; set; }

    /// <summary>
    /// Gets or sets the user ID who locked the entity.
    /// This identifies which user currently has the entity locked for editing.
    /// </summary>
    public Guid? UserLockedId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant applications.
    /// This is used to distinguish entities belonging to different tenants in a multi-tenant architecture.
    /// </summary>
    public Guid? TenantId { get; set; }

    #region Navigation Properties

    /// <summary>
    /// Navigation property to the user who locked the entity.
    /// This could be useful to fetch details about the user who locked the entity.
    /// </summary>
    public virtual User? LockedByUser { get; set; }
    
    #endregion
}
