// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Stations.Models;
using server.src.Domain.Geography.Natural.ClimateZones.Models;
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;
using server.src.Domain.Geography.Natural.Timezones.Models;

namespace server.src.Domain.Geography.Natural.Locations.Models;

/// <summary>
/// Represents a geographic location with detailed attributes, including longitude, latitude, altitude, and region-specific information.
/// </summary>
public class Location : BaseEntity
{
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
    /// Gets or sets the depth of the location in meters below sea level.
    /// </summary>
    public double Depth { get; set; }

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
    /// Gets or sets the foreign key for the surface type.
    /// </summary>
    public Guid SurfaceTypeId { get; set; }

    /// <summary>
    /// Gets or sets the foreign key for the climate zone.
    /// </summary>
    public Guid ClimateZoneId { get; set; }

    /// <summary>
    /// Foreign key for the associated natural feature (e.g., a mountain, river, island).
    /// </summary>
    public Guid NaturalFeatureId { get; set; }

    /// <summary>
    /// Foreign key for the associated station.
    /// </summary>
    public Guid? StationId { get; set; }

    #endregion

    #region Navigation properties

    /// <summary>
    /// Gets or sets the associated time zone of the location.
    /// </summary>
    public virtual Timezone Timezone { get; set; }

    /// <summary>
    /// Gets or sets the associated surface type of the location.
    /// </summary>
    public virtual SurfaceType SurfaceType { get; set; }

    /// <summary>
    /// Gets or sets the associated climate zone of the location.
    /// </summary>
    public virtual ClimateZone ClimateZone { get; set; }

    /// <summary>
    /// Navigation property to the associated natural feature.
    /// </summary>
    public virtual NaturalFeature NaturalFeature { get; set; }

    /// <summary>
    /// Navigation property to the station.
    /// </summary>
    public virtual Station? Station { get; set; }

    #endregion
}
