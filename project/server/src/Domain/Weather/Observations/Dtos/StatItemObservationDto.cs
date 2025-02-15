namespace server.src.Domain.Weather.Observations.Dtos;

/// <summary>
/// Represents an observation item containing minimal statistical data.
/// </summary>
public record StatItemObservationDto(
    Guid Id, 
    string Timestamp, 
    int TemperatureC
);