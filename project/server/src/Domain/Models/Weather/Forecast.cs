using server.src.Domain.Models.Geography;

namespace server.src.Domain.Models.Weather;

/// <summary>
/// Represents a weather forecast for a specific date, including temperature and a summary.
/// This class is used to store forecast data, including associated warning information.
/// </summary>
public class Forecast
{
    /// <summary>
    /// Gets or sets the unique identifier for the forecast.
    /// This is used to uniquely identify the forecast entry in the system.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the date of the forecast.
    /// This represents the specific date the forecast is valid for.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the temperature in Celsius.
    /// This is the forecasted temperature for the given date.
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// Gets the temperature in Fahrenheit.
    /// </summary>
    public int TemperatureF => TemperatureC * 9 / 5 + 32;

    /// <summary>
    /// Gets or sets the "feels like" temperature in Celsius, considering humidity and wind chill.
    /// </summary>
    public int FeelsLikeC { get; set; }

    /// <summary>
    /// Gets or sets the humidity percentage.
    /// This represents the expected humidity level for the given date, ranging from 0 to 100.
    /// </summary>
    public int Humidity { get; set; }

    /// <summary>
    /// Gets or sets the wind speed in kilometers per hour.
    /// This indicates the predicted wind speed for the given date.
    /// </summary>
    public double WindSpeedKph { get; set; }

    /// <summary>
    /// Gets or sets the wind direction in degrees (0°-360°).
    /// This represents the direction from which the wind is blowing.
    /// </summary>
    public int WindDirection { get; set; }

    /// <summary>
    /// Gets or sets the atmospheric pressure in hPa (hectopascals).
    /// This represents the expected pressure at sea level for the given date.
    /// </summary>
    public double PressureHpa { get; set; }

    /// <summary>
    /// Gets or sets the amount of precipitation in millimeters.
    /// This indicates the expected rainfall or snowfall for the given date.
    /// </summary>
    public double PrecipitationMm { get; set; }

    /// <summary>
    /// Gets or sets the visibility distance in kilometers.
    /// This represents how far objects can be seen under current weather conditions.
    /// </summary>
    public double VisibilityKm { get; set; }

    /// <summary>
    /// Gets or sets the UV index.
    /// This measures the risk of harm from unprotected sun exposure, typically ranging from 0 (low) to 11+ (extreme).
    /// </summary>
    public int UVIndex { get; set; }

    /// <summary>
    /// Gets or sets the air quality index (AQI).
    /// This provides an indication of air pollution levels, typically ranging from 0 (good) to 500 (hazardous).
    /// </summary>
    public int AirQualityIndex { get; set; }

    /// <summary>
    /// Gets or sets the percentage of cloud cover.
    /// This represents the estimated cloudiness, ranging from 0 (clear sky) to 100 (completely overcast).
    /// </summary>
    public int CloudCover { get; set; }

    /// <summary>
    /// Gets or sets the probability of lightning strikes (0-100%).
    /// </summary>
    public int LightningProbability { get; set; }

    /// <summary>
    /// Gets or sets the pollen count, measuring airborne pollen levels.
    /// </summary>
    public int PollenCount { get; set; }

    /// <summary>
    /// Gets or sets the sunrise time.
    /// </summary>
    public TimeSpan Sunrise { get; set; }

    /// <summary>
    /// Gets or sets the sunset time.
    /// </summary>
    public TimeSpan Sunset { get; set; }

    /// <summary>
    /// Gets or sets the summary or description of the weather.
    /// This typically provides a brief overview of the expected weather conditions (e.g., sunny, cloudy, rainy).
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the forecast has been read.
    /// This can be used to track if a user has viewed the forecast.
    /// </summary>
    public bool IsRead { get; set; }

    /// <summary>
    /// Gets or sets the version GUID for optimistic concurrency control.
    /// </summary>
    public Guid Version { get; set; }

    #region Foreign Keys
    /// <summary>
    /// Gets or sets the identifier for the associated warning.
    /// This represents the warning associated with the forecast, such as extreme weather conditions.
    /// </summary>
    public Guid WarningId { get; set; }

    /// <summary>
    /// Gets or sets the moon phase identifier.
    /// This represents the foreign key linking to the MoonPhase entity.
    /// </summary>
    public Guid MoonPhaseId { get; set; }

    /// <summary>
    /// Gets or sets the location identifier.
    /// This represents the foreign key linking to the Location entity.
    /// </summary>
    public Guid LocationId { get; set; }
    #endregion

    #region Navigation Properties
    /// <summary>
    /// Gets or sets the associated warning for the forecast.
    /// This links the forecast to a warning (if any) for extreme weather conditions on that day.
    /// </summary>
    public Warning Warning { get; set; }

    /// <summary>
    /// Gets or sets the moon phase entity.
    /// This links the forecast to the corresponding moon phase.
    /// </summary>
    public MoonPhase MoonPhase { get; set; }

    /// <summary>
    /// Gets or sets the location entity.
    /// This links the forecast to the corresponding location.
    /// </summary>
    public Location Location { get; set; }
    #endregion
}
