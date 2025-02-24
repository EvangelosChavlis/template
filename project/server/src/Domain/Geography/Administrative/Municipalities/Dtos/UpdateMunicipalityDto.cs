namespace server.src.Domain.Geography.Administrative.Municipalities.Dtos;

/// <summary>
/// DTO for updating an existing municipality. Used to encapsulate 
/// the data for updating a municipality's details.
/// </summary>
public record UpdateMunicipalityDto(
    string Name,
    string Description,
    long Population,
    bool IsActive,
    Guid RegionId,
    Guid Version
);