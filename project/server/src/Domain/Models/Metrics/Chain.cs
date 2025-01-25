namespace server.src.Domain.Models.Metrics;

/// <summary>
/// Represents a connection between two audit log entries,  
/// forming a historical chain of changes for an entity.  
/// This allows tracking of how an entity's state evolves over time.  
/// </summary>
public class Chain
{
    /// <summary>
    /// Gets or sets the unique identifier of the chain entry.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when this chain entry was created.
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
    /// Gets or sets the source audit log entry in this chain.
    /// </summary>
    public AuditLog? SourceAuditLog { get; set; }

    /// <summary>
    /// Gets or sets the target audit log entry in this chain.
    /// </summary>
    public AuditLog? TargetAuditLog { get; set; }
    #endregion
}
