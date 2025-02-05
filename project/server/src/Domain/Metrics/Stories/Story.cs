// source
using server.src.Domain.Metrics.TelemetryRecords.Models;

namespace server.src.Domain.Metrics.Stories;

/// <summary>
/// Represents a story entry that tracks telemetry records associated with a user.
/// This entity links a sequence of telemetry records, showing transitions between different states.
/// </summary>
public class Story
{
    /// <summary>
    /// Gets or sets the unique identifier for the story record.
    /// This serves as the primary key for the story entity.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the story record was created.
    /// This indicates when the story entry was logged.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    #region Foreign keys

    /// <summary>
    /// Gets or sets the unique identifier of the source telemetry record.
    /// This represents the previous state in the telemetry sequence.
    /// </summary>
    public Guid? SourceTelemetryRecordId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the target telemetry record.
    /// This represents the next state in the telemetry sequence.
    /// </summary>
    public Guid? TargetTelemetryRecordId { get; set; }

    #endregion

    #region Navigation properties

    /// <summary>
    /// Gets or sets the source telemetry record associated with this story.
    /// This represents the previous telemetry state.
    /// </summary>
    public TelemetryRecord? SourceTelemetryRecord { get; set; }

    /// <summary>
    /// Gets or sets the target telemetry record associated with this story.
    /// This represents the new telemetry state.
    /// </summary>
    public TelemetryRecord? TargetTelemetryRecord { get; set; }

    #endregion
}
