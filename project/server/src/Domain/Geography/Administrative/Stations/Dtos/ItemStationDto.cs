namespace server.src.Domain.Geography.Administrative.Stations.Dtos;

/// <summary>
/// DTO for a detailed view of a station. Includes all relevant 
/// details about a station, such as name, description, capital, area, and active status.
/// </summary>
public record ItemStationDto(
    Guid Id,
    string Name,
    string Description,
    string Code,
    string Location,
    bool IsActive,
    Guid Version
);