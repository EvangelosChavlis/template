namespace server.src.Domain.Geography.Administrative.Regions.Dtos;

/// <summary>
/// DTO for listing regions. Provides a summarized view of a 
/// region for display in lists, including name and area.
/// </summary>
public record ListItemRegionDto(
    Guid Id,
    string Name,
    string Code,
    double AreaKm2,
    bool IsActive,
    int Count
);