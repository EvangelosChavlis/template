// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.Locations.Models;
using server.src.Domain.Weather.MoonPhases.Models;

namespace server.src.Domain.Weather.Observations.Models;

/// <summary>
/// Represents a recorded weather observation for a specific date and location.
/// Observations are used as input data for generating forecasts.
/// </summary>
public class Observation : BaseEntity
{
    /// <summary>
    /// Gets or sets the date and time of the observation.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the recorded temperature in Celsius.
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// Gets the temperature in Fahrenheit.
    /// </summary>
    public int TemperatureF => TemperatureC * 9 / 5 + 32;

    /// <summary>
    /// Gets or sets the recorded humidity percentage (0-100).
    /// </summary>
    public int Humidity { get; set; }

    /// <summary>
    /// Gets or sets the recorded wind speed in kilometers per hour.
    /// </summary>
    public double WindSpeedKph { get; set; }

    /// <summary>
    /// Gets or sets the wind direction in degrees (0°-360°).
    /// </summary>
    public int WindDirection { get; set; }

    /// <summary>
    /// Gets or sets the recorded atmospheric pressure in hPa.
    /// </summary>
    public double PressureHpa { get; set; }

    /// <summary>
    /// Gets or sets the recorded precipitation in millimeters.
    /// </summary>
    public double PrecipitationMm { get; set; }

    /// <summary>
    /// Gets or sets the recorded visibility distance in kilometers.
    /// </summary>
    public double VisibilityKm { get; set; }

    /// <summary>
    /// Gets or sets the UV index recorded at the time of observation.
    /// </summary>
    public int UVIndex { get; set; }

    /// <summary>
    /// Gets or sets the air quality index (AQI) recorded at the time of observation.
    /// </summary>
    public int AirQualityIndex { get; set; }

    /// <summary>
    /// Gets or sets the percentage of cloud cover at the time of observation.
    /// </summary>
    public int CloudCover { get; set; }

    /// <summary>
    /// Gets or sets the probability of lightning strikes (0-100%).
    /// </summary>
    public int LightningProbability { get; set; }

    /// <summary>
    /// Gets or sets the pollen count at the time of observation.
    /// </summary>
    public int PollenCount { get; set; }

    /// <summary>
    /// Gets or sets the associated moon phase identifier.
    /// </summary>
    public Guid MoonPhaseId { get; set; }

    /// <summary>
    /// Gets or sets the location identifier.
    /// </summary>
    public Guid LocationId { get; set; }

    #region Navigation properties

    /// <summary>
    /// Gets or sets the associated moon phase for the observation.
    /// </summary>
    public virtual MoonPhase MoonPhase { get; set; }

    /// <summary>
    /// Gets or sets the location where the observation was recorded.
    /// </summary>
    public virtual Location Location { get; set; }

    #endregion
}
