namespace server.src.Domain.Weather.Collections.Observations.Dtos;

/// <summary>
/// Represents an observation item containing minimal statistical data.
/// </summary>
public record StatItemObservationDto(
    Guid Id, 
    string Timestamp, 
    int TemperatureC
);