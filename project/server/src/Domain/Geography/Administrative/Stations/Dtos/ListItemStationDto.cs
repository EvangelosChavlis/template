namespace server.src.Domain.Geography.Administrative.Stations.Dtos;

/// <summary>
/// DTO for listing stations. Used for displaying a 
// summarized view of a station in lists, including name, status, and population count.
/// </summary>
public record ListItemStationDto(
    Guid Id,
    string Name,
    string Code,
    bool IsActive,
    int Observations,
    int Forecasts
);