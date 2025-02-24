// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.Locations.Models;
using server.src.Domain.Weather.Forecasts.Models;

namespace server.src.Domain.Weather.Reports.Models;

/// <summary>
/// A weather report summarizing the forecast data over a period.
/// </summary>
public class Report : BaseEntity
{
    /// <summary>
    /// Title of the report, typically describing the weather conditions.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// The start date of the forecast period for this report.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// The end date of the forecast period for this report.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// The weather summary for the period (can be derived from Forecasts).
    /// </summary>
    public string WeatherSummary { get; set; }

    // Aggregate data (calculated based on linked Forecasts)
    
    /// <summary>
    /// The average temperature in Celsius for the period.
    /// </summary>
    public double AverageTemperatureC { get; set; }

    /// <summary>
    /// The total precipitation in millimeters for the period.
    /// </summary>
    public double TotalPrecipitationMm { get; set; }

    /// <summary>
    /// The average wind speed in kilometers per hour for the period.
    /// </summary>
    public double AverageWindSpeedKph { get; set; }

    /// <summary>
    /// The average humidity for the period.
    /// </summary>
    public double AverageHumidity { get; set; }

    // Navigation properties

    /// <summary>
    /// The ID of the location the report is based on.
    /// </summary>
    public Guid LocationId { get; set; }

    /// <summary>
    /// Navigation property to the location entity.
    /// </summary>
    public virtual Location Location { get; set; }

    /// <summary>
    /// A collection of Forecasts that are included in this report.
    /// </summary>
    public virtual List<Forecast> Forecasts { get; set; }
}
