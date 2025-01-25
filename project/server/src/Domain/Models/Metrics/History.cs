using server.src.Domain.Models.Auth;

namespace server.src.Domain.Models.Metrics;

/// <summary>
/// Represents a history entry that tracks telemetry records associated with a user.
/// This entity links a sequence of telemetry records, showing transitions between different states.
/// </summary>
public class History
{
    /// <summary>
    /// Gets or sets the unique identifier for the history record.
    /// This serves as the primary key for the history entity.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the history record was created.
    /// This indicates when the history entry was logged.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    #region Foreign Keys
    /// <summary>
    /// Gets or sets the unique identifier of the source telemetry record.
    /// This represents the previous state in the telemetry sequence.
    /// </summary>
    public Guid? SourceTelemetryId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the target telemetry record.
    /// This represents the next state in the telemetry sequence.
    /// </summary>
    public Guid? TargetTelemetryId { get; set; }
    #endregion

    #region Navigation Properties
    /// <summary>
    /// Gets or sets the source telemetry record associated with this history.
    /// This represents the previous telemetry state.
    /// </summary>
    public Telemetry? SourceTelemetry { get; set; }

    /// <summary>
    /// Gets or sets the target telemetry record associated with this history.
    /// This represents the new telemetry state.
    /// </summary>
    public Telemetry? TargetTelemetry { get; set; }
    #endregion
}
