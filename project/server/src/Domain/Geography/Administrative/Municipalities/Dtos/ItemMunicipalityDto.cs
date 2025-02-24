namespace server.src.Domain.Geography.Administrative.Municipalities.Dtos;

/// <summary>
/// DTO for a detailed view of a municipality. Includes all 
/// relevant details about a municipality, such as name, description, population, 
/// </summary>
public record ItemMunicipalityDto(
    Guid Id,
    string Name,
    string Description,
    long Population,
    bool IsActive,
    Guid RegionId,
    Guid Version
);