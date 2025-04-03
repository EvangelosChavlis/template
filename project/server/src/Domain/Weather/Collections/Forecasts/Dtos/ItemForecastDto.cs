namespace server.src.Domain.Weather.Collections.Forecasts.Dtos;

/// <summary>
/// Represents a detailed forecast item containing the full date, 
/// temperature, summary, and warning details.
/// </summary>
public record ItemForecastDto(
    Guid Id,
    string Date,
    int TemperatureC,
    int TemperatureF,
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
    string Warning,
    string MoonPhase,
    string Station,
    Guid Version
);