// source
using server.src.Domain.Geography.ClimateZones.Models;
using server.src.Domain.Geography.TerrainTypes.Models;
using server.src.Domain.Weather.Forecasts.Models;
using server.src.Domain.Geography.Timezones.Models;

namespace server.src.Domain.Geography.Locations.Models;

/// <summary>
/// Represents a geographic location with detailed attributes, including longitude, latitude, altitude, and region-specific information.
/// </summary>
public class Location
{
    /// <summary>
    /// Gets or sets the unique identifier for the location.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate of the location.
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Gets or sets the latitude coordinate of the location.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Gets or sets the altitude of the location in meters above sea level.
    /// </summary>
    public double Altitude { get; set; }

    /// <summary>
    /// Gets or sets the status indicating if the location is active.
    /// Determines whether the location is enabled for assignment or usage.
    /// </summary>
    public bool IsActive { get; set; }

    #region Foreign keys

    /// <summary>
    /// Gets or sets the foreign key for the time zone.
    /// </summary>
    public Guid TimezoneId { get; set; }

    /// <summary>
    /// Gets or sets the foreign key for the terrain type.
    /// </summary>
    public Guid TerrainTypeId { get; set; }

    /// <summary>
    /// Gets or sets the foreign key for the climate zone.
    /// </summary>
    public Guid ClimateZoneId { get; set; }

    #endregion

    #region Navigation properties

    /// <summary>
    /// Gets or sets the associated time zone of the location.
    /// </summary>
    public virtual Timezone Timezone { get; set; }

    /// <summary>
    /// Gets or sets the associated terrain type of the location.
    /// </summary>
    public virtual TerrainType TerrainType { get; set; }

    /// <summary>
    /// Gets or sets the associated climate zone of the location.
    /// </summary>
    public virtual ClimateZone ClimateZone { get; set; }

    /// <summary>
    /// Gets or sets the list of forecasts associated with this location.
    /// </summary>
    public virtual List<Forecast> Forecasts { get; set; }

    #endregion
}
