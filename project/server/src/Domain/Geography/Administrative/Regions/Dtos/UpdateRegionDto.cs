namespace server.src.Domain.Geography.Administrative.Regions.Dtos;

/// <summary>
/// DTO for updating an existing region. Used to encapsulate 
/// the data for updating a region's details.
/// </summary>
public record UpdateRegionDto(
    string Name,
    string Description,
    double AreaKm2,
    string Code,
    bool IsActive,
    Guid StateId,
    Guid Version
);