using server.src.Domain.Common.Models;

namespace server.src.Domain.Common.Extensions;

public static class EntityLockExtensions
{
    /// <summary>
    /// Checks if the entity is not locked.
    /// </summary>
    /// <param name="entity">The entity with the LockUntil property.</param>
    /// <returns>True if the entity is not locked; otherwise, false.</returns>
    public static bool IsNotLocked(this BaseEntity entity)
    {
        return !entity.LockUntil.HasValue || entity.LockUntil <= DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if the entity is currently locked.
    /// </summary>
    /// <param name="entity">The entity with the LockUntil property.</param>
    /// <returns>True if the entity is locked; otherwise, false.</returns>
    public static bool IsLocked(this BaseEntity entity)
    {
        return entity.LockUntil.HasValue && entity.LockUntil > DateTime.UtcNow;
    }
}
