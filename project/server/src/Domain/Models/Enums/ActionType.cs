namespace server.src.Domain.Models.Enums;

/// <summary>
/// Represents the type of action performed on an entity.
/// </summary>
public enum ActionType
{
    /// <summary>
    /// Indicates that a new entity was created.
    /// </summary>
    Created,

    /// <summary>
    /// Indicates that an existing entity was updated.
    /// </summary>
    Updated,

    /// <summary>
    /// Indicates that an entity was deleted.
    /// </summary>
    Deleted
}
