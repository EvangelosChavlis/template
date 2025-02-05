// source
using server.src.Domain.Auth.Users.Models;
using server.src.Domain.Metrics.AuditLogs.Enums;
using server.src.Domain.Metrics.Trails;

namespace server.src.Domain.Metrics.AuditLogs.Models;

/// <summary>
/// Represents an audit log entry for tracking changes to entities in the system.
/// This includes details about the entity affected, the action performed, and the user responsible.
/// </summary>
public class AuditLog
{
    /// <summary>
    /// Gets or sets the unique identifier of the audit log entry.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the entity being changed (e.g., User.Id, Product.Id).
    /// </summary>
    public Guid EntityId { get; set; }

    /// <summary>
    /// Gets or sets the type of the entity being affected (e.g., User, Order).
    /// The value is constrained to predefined enum values.
    /// </summary>
    public string EntityType { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the action occurred.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the action that was performed (e.g., Created, Updated, Deleted).
    /// The value is constrained to predefined enum values.
    /// </summary>
    public ActionType Action { get; set; }

    /// <summary>
    /// Gets or sets the IP address from which the action was performed.
    /// </summary>
    public string? IPAddress { get; set; }

    /// <summary>
    /// Gets or sets the reason for the action, if applicable (e.g., "User requested change").
    /// </summary>
    public string? Reason { get; set; }

    /// <summary>
    /// Gets or sets additional metadata about the action (e.g., comments, tags, business rules).
    /// </summary>
    public string? AdditionalMetadata { get; set; }

    /// <summary>
    /// Gets or sets the values of the entity before the change was made (in JSON or serialized format).
    /// </summary>
    public string? BeforeValues { get; set; }

    /// <summary>
    /// Gets or sets the values of the entity after the change was made (in JSON or serialized format).
    /// </summary>
    public string? AfterValues { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the action was performed by a system or an admin.
    /// </summary>
    public bool IsSystemAction { get; set; }

    #region Foreign Keys
    /// <summary>
    /// Gets or sets the unique identifier of the user who performed the action.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the related history entry.  
    /// This links the audit log to a broader historical context.
    /// </summary>
    public Guid TelemetryId { get; set; }
    #endregion

    #region Navigation properties
    
    /// <summary>
    /// Gets or sets the user entity associated with this audit log.
    /// </summary>
    public virtual User User { get; set; }

    /// <summary>
    /// Gets or sets the telemetry data associated with this audit log.
    /// </summary>
    public virtual Telemetry Telemetry { get; set; }

    /// <summary>
    /// Gets or sets the trail entity associated with this audit log.
    /// </summary>
    public virtual List<Trail> Trails { get; set; }
    
    #endregion
}