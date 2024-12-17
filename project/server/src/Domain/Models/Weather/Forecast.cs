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
    /// Gets or sets the summary or description of the weather.
    /// This typically provides a brief overview of the expected weather conditions (e.g., sunny, cloudy, rainy).
    /// </summary>
    public string Summary { get; set; }

    #region Foreign Keys
    /// <summary>
    /// Gets or sets the identifier for the associated warning.
    /// This represents the warning associated with the forecast, such as extreme weather conditions.
    /// </summary>
    public Guid WarningId { get; set; }
    #endregion

    #region Navigation Properties
    /// <summary>
    /// Gets or sets the associated warning for the forecast.
    /// This links the forecast to a warning (if any) for extreme weather conditions on that day.
    /// </summary>
    public Warning Warning { get; set; }
    #endregion
}