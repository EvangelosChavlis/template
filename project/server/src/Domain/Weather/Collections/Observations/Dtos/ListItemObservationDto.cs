namespace server.src.Domain.Weather.Collections.Observations.Dtos;

/// <summary>
/// Represents a simplified observation item containing basic meteorological data.
/// </summary>
public record ListItemObservationDto(
    Guid Id, 
    string Timestamp, 
    int TemperatureC,
    int Humidity,
    string MoonPhase,
    string Station
);