namespace server.src.Domain.Geography.Administrative.Municipalities.Dtos;

/// <summary>
/// DTO for creating a new municipality. Used to encapsulate the 
/// necessary data for creating a municipality.
/// </summary>
public record CreateMunicipalityDto(
    string Name,
    string Description,
    long Population,
    Guid RegionId
);