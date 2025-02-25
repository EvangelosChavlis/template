namespace server.src.Domain.Geography.Administrative.Regions.Dtos;

/// <summary>
/// DTO for a detailed view of a region. Includes all relevant 
/// details about a region, such as name, description, area.
/// </summary>
public record ItemRegionDto(
    Guid Id,
    string Name,
    string Description,
    double AreaKm2,
    bool IsActive,
    string State,
    Guid Version
);