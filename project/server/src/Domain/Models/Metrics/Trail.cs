namespace server.src.Domain.Models.Metrics;

/// <summary>
/// Represents a historical trail of audit log entries,  
/// forming a sequential record of changes for an entity.  
/// This allows tracking of how an entity's state evolves over time.  
/// </summary>
public class Trail
{
    /// <summary>
    /// Gets or sets the unique identifier of the trail entry.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when this trail entry was created.
    /// </summary>
    public DateTime Timestamp { get; set; }

    #region Foreign Keys
    /// <summary>
    /// Gets or sets the unique identifier of the source audit log entry.  
    /// This represents the previous state in the entity's change history.
    /// </summary>
    public Guid? SourceAuditLogId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the target audit log entry.  
    /// This represents the next state in the entity's change history.
    /// </summary>
    public Guid? TargetAuditLogId { get; set; }
    #endregion

    #region Navigation Properties
    /// <summary>
    /// Gets or sets the source audit log entry in this trail.
    /// </summary>
    public AuditLog? SourceAuditLog { get; set; }

    /// <summary>
    /// Gets or sets the target audit log entry in this trail.
    /// </summary>
    public AuditLog? TargetAuditLog { get; set; }
    #endregion
}
