namespace server.src.Domain.Weather.Collections.Observations.Dtos;

/// <summary>
/// Represents a data transfer object for updating a weather observation.
/// </summary>
public record UpdateObservationDto(
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
    Guid StationId,
    Guid MoonPhaseId,
    Guid Version
);