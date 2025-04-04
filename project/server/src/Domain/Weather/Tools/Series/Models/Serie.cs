// source
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Tools.Sensors.Models;

namespace server.src.Domain.Weather.Tools.Series.Models;

/// <summary>
/// Represents a time series data point recorded from a sensor.
/// </summary>
public class Serie : BaseEntity
{
    /// <summary>
    /// Gets or sets the timestamp of the recorded data.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the recorded value from the sensor.
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Gets or sets remarks associated with the data point.
    /// </summary>
    public string Remarks { get; set; }

    #region Foreign Keys

    /// <summary>
    /// Gets or sets the foreign key for the associated sensor.
    /// </summary>
    public Guid SensorId { get; set; }

    #endregion

    #region Navigation Properties

    /// <summary>
    /// Gets or sets the associated sensor.
    /// </summary>
    public virtual Sensor Sensor { get; set; }

    #endregion
}