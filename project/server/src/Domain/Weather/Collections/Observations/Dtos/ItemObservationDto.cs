namespace server.src.Domain.Weather.Collections.Observations.Dtos;

/// <summary>
/// Represents a detailed observation item containing all recorded meteorological data.
/// </summary>
public record ItemObservationDto(
    Guid Id,
    string Timestamp,
    int TemperatureC,
    int TemperatureF,
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
    string MoonPhase,
    string Station,
    Guid Version
);