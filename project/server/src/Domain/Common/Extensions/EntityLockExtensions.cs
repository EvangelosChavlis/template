// source
using server.src.Domain.Common.Models;

namespace server.src.Domain.Common.Extensions;

public static class EntityLockExtensions
{
    /// <summary>
    /// Checks if the entity is currently locked.
    /// </summary>
    /// <param name="entity">The entity with the LockUntil property.</param>
    /// <returns>True if the entity is locked; otherwise, false.</returns>
    public static bool IsLocked(this BaseEntity entity)
    {
        return entity.LockUntil.HasValue && 
            entity.LockUntil > DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if the entity is locked by another user.
    /// </summary>
    /// <param name="entity">The entity to check.</param>
    /// <param name="currentUserId">The current user's ID.</param>
    /// <returns>True if another user has locked the entity; otherwise, false.</returns>
    public static bool IsLockedByOtherUser(this BaseEntity entity, Guid currentUserId)
    {
        if (!entity.IsLocked()) return false;

        if (!entity.UserLockedId.HasValue) return true;

        return entity.UserLockedId != currentUserId;
    }
}
