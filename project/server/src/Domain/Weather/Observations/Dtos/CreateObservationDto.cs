namespace server.src.Domain.Weather.Observations.Dtos;

/// <summary>
/// Represents a data transfer object for creating a weather observation.
/// </summary>
public record CreateObservationDto(
    DateTime Timestamp,
    int TemperatureC,
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
    Guid LocationId,
    Guid MoonPhaseId
);