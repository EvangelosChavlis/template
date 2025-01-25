namespace server.src.Domain.Models.Changes;

/// <summary>
/// Represents an individual change entry linked to a change log.
/// </summary>
public class Change
{
    /// <summary>
    /// Gets or sets the unique identifier for the change entry.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the change.
    /// Provides a brief title or identifier for the modification.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the change.
    /// Contains details about what was modified in the system.
    /// </summary>
    public string Description { get; set; }

    #region Foreign Keys
    /// <summary>
    /// Gets or sets the identifier of the associated change log.
    /// Links this change to a specific change log entry.
    /// </summary>
    public Guid ChangeLogId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the associated change type.
    /// Defines the type of modification that was performed.
    /// </summary>
    public Guid ChangeTypeId { get; set; }
    #endregion

    #region Navigation Properties
    /// <summary>
    /// Gets or sets the related change log entry.
    /// Establishes the relationship between this change and its change log.
    /// </summary>
    public ChangeLog ChangeLog { get; set; }

    /// <summary>
    /// Gets or sets the related change type.
    /// Specifies the type of change, such as an addition, update, or deletion.
    /// </summary>
    public ChangeType ChangeType { get; set; }
    #endregion
}