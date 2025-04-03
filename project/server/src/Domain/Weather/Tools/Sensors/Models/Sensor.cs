// source
using server.src.Domain.Geography.Administrative.Stations.Models;
using server.src.Domain.Weather.Tools.Units.Models;
using server.src.Domain.Weather.Tools.HealthStatuses.Models;
using server.src.Domain.Common.Models;

namespace server.src.Domain.Weather.Tools.Sensors.Models;

/// <summary>
/// Represents a sensor device used in a weather station to record data.
/// </summary>
public class Sensor : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the sensor (e.g., "Temperature", "Wind").
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the manufacturer of the sensor.
    /// </summary>
    public string Manufacturer { get; set; }

    /// <summary>
    /// Gets or sets the serial number (SN) of the sensor.
    /// </summary>
    public string SN { get; set; }

    /// <summary>
    /// Gets or sets the current value measured by the sensor (nullable).
    /// </summary>
    public double? CurreantValue { get; set; }

    public DateTime Timestamp { get; set; }

     /// <summary>
    /// Gets or sets a value indicating whether the sensor is active.
    /// </summary>
    public bool IsActive { get; set; }

    #region Foreign Keys

    /// <summary>
    /// Gets or sets the foreign key for the associated unit.
    /// </summary>
    public Guid UnitId { get; set; }

    /// <summary>
    /// Gets or sets the foreign key for the associated health status.
    /// </summary>
    public Guid HealthStatusId { get; set; }

    /// <summary>
    /// Gets or sets the foreign key for the associated station.
    /// </summary>
    public Guid StationId { get; set; }

    #endregion

    #region Navigation Properties

    /// <summary>
    /// Gets or sets the associated measurement unit.
    /// </summary>
    public virtual Unit Unit { get; set; }

    /// <summary>
    /// Gets or sets the associated health status.
    /// </summary>
    public virtual HealthStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the associated station.
    /// </summary>
    public virtual Station Station { get; set; }

    #endregion
}