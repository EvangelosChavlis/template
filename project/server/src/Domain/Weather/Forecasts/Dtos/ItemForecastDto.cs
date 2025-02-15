namespace server.src.Domain.Weather.Forecasts.Dtos;

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
    double Longitude,
    double Latitude,
    double Altitude,
    string Summary,
    string Warning,
    string MoonPhase,
    string Location,
    Guid Version
);