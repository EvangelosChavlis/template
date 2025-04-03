// source
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Tools.Sensors.Models;

namespace server.src.Domain.Weather.Tools.HealthStatuses.Models;

/// <summary>
/// Represents the health status of a sensor.
/// </summary>
public class HealthStatus : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the health status (e.g., "Operational", "Faulty").
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a brief description of the health status.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the code of the health status (e.g., "OP", "FT").
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the status is active.
    /// </summary>
    public bool IsActive { get; set; }

    #region Navigation Properties

    /// <summary>
    /// Gets or sets the list of sensors associated with this healthstatus.
    /// </summary>
    public virtual List<Sensor> Sensors { get; set; }

    #endregion
}
