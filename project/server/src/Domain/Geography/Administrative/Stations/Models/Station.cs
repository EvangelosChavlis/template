// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;
using server.src.Domain.Geography.Natural.Locations.Models;
using server.src.Domain.Weather.Collections.Forecasts.Models;
using server.src.Domain.Weather.Collections.Observations.Models;
using server.src.Domain.Weather.Tools.Sensors.Models;

namespace server.src.Domain.Geography.Administrative.Stations.Models;

/// <summary>
/// Represents a weather station that records observations, linked to a specific location.
/// </summary>
public class Station : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the station.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a brief description of the station.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the code of the station (e.g., station ID or unique code).
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the station is active.
    /// </summary>
    public bool IsActive { get; set; }

    #region Foreign keys

    /// <summary>
    /// Gets or sets the foreign key for the location where the station is situated.
    /// </summary>
    public Guid LocationId { get; set; }

    /// <summary>
    /// Gets or sets the foreign key for the neighborhood that the station belongs to (optional).
    /// </summary>
    public Guid? NeighborhoodId { get; set; }

    #endregion

    #region Navigation properties

    /// <summary>
    /// Gets or sets the associated location of the station.
    /// </summary>
    public virtual Location Location { get; set; }

    /// <summary>
    /// Gets or sets the associated neighborhood of the station (nullable).
    /// </summary>
    public virtual Neighborhood? Neighborhood { get; set; }

    /// <summary>
    /// Gets or sets the list of forecasts associated with this location.
    /// </summary>
    public virtual List<Forecast> Forecasts { get; set; }

    /// <summary>
    /// Gets or sets the list of observations associated with this location.
    /// </summary>
    public virtual List<Observation> Observations { get; set; }

    /// <summary>
    /// Gets or sets the list of sensors associated with this station.
    /// </summary>
    public virtual List<Sensor> Sensors { get; set; }
    #endregion
}