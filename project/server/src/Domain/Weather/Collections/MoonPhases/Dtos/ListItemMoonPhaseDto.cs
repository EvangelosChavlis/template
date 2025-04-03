namespace server.src.Domain.Weather.Collections.MoonPhases.Dtos;

/// <summary>
/// Represents a simplified moon phase item that contains basic details, 
/// including the number of related forecasts.
/// </summary>
public record ListItemMoonPhaseDto(
    Guid Id, 
    string Name, 
    string Code,
    int ForecastCount,
    int ObservationCount
);