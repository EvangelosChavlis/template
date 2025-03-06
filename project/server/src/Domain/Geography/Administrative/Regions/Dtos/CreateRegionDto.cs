namespace server.src.Domain.Geography.Administrative.Regions.Dtos;

/// <summary>
/// DTO for creating a new region. Used to encapsulate 
/// the necessary data for creating a region.
/// </summary>
public record CreateRegionDto(
    string Name,
    string Description,
    double AreaKm2,
    string Code,
    bool IsActive,
    Guid StateId
);