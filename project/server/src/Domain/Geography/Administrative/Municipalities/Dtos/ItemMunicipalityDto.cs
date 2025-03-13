namespace server.src.Domain.Geography.Administrative.Municipalities.Dtos;

/// <summary>
/// DTO for a detailed view of a municipality. Includes all 
/// relevant details about a municipality, such as name, description, population, 
/// </summary>
public record ItemMunicipalityDto(
    Guid Id,
    string Name,
    string Description,
    double AreaKm2,
    long Population,
    string Code,
    bool IsActive,
    string Region,
    Guid Version
);