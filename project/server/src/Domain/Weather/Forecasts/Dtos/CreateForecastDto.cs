namespace server.src.Domain.Weather.Forecasts.Dtos;

/// <summary>
/// Represents a forecast data transfer object containing detailed 
/// meteorological information, including temperature, humidity, wind, 
/// atmospheric pressure, and related warnings.
/// </summary>
public record CreateForecastDto(
    DateTime Date, 
    int TemperatureC,
    int FeelsLikeC,
    int Humidity,
    double WindSpeedKph,
    int WindDirection,
    double PressureHpa,
    double PrecipitationMm,
    double VisibilityKm,
    int UVIndex,
    int AirQualityIndex,
    int CloudCover,
    int LightningProbability,
    int PollenCount,
    TimeSpan Sunrise,
    TimeSpan Sunset,
    string Summary,
    Guid LocationId,
    Guid WarningId,
    Guid MoonPhaseId
);

