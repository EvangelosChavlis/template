// source
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Tools.Sensors.Models;

namespace server.src.Domain.Weather.Tools.Units.Models;

/// <summary>
/// Represents a measurement unit, including its name, symbol, description, and active status.
/// </summary>
public class Unit : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the unit (e.g., "Celsius", "Kilometers per hour").
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the symbol of the unit (e.g., "°C", "km/h").
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets a brief description of the unit.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the unit is active.
    /// </summary>
    public bool IsActive { get; set; }

    #region Navigation Properties

    /// <summary>
    /// Gets or sets the list of sensors associated with this unit.
    /// </summary>
    public virtual List<Sensor> Sensors { get; set; }

    #endregion
}