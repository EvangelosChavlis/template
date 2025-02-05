// source
using server.src.Domain.Geography.Locations.Models;

namespace server.src.Domain.Geography.ClimateZones.Models;

/// <summary>
/// Represents a climate zone, such as tropical, temperate, arid, or polar.
/// </summary>
public class ClimateZone
{
    /// <summary>
    /// Gets or sets the unique identifier for the climate zone.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the climate zone (e.g., Tropical, Temperate, Arid).
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a description of the climate zone characteristics.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the average annual temperature in Celsius.
    /// </summary>
    public double AvgTemperatureC { get; set; }

    /// <summary>
    /// Gets or sets the average annual precipitation in millimeters.
    /// </summary>
    public double AvgPrecipitationMm { get; set; }

    /// <summary>
    /// Gets or sets the status indicating if the climate zone is active.
    /// Determines whether the climate zone is enabled for assignment or usage.
    /// </summary>
    public bool IsActive { get; set; }

    #region Navigation properties

    /// <summary>
    /// Gets or sets the list of locations associated with this climate zone.
    /// </summary>
    public virtual List<Location> Locations { get; set; }
    
    #endregion
}