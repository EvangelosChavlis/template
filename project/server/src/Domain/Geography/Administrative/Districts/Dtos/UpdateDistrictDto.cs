namespace server.src.Domain.Geography.Administrative.Districts.Dtos;

/// <summary>
/// DTO for updating an existing district. Used to encapsulate 
/// the data for updating a district's details.
/// </summary>
public record UpdateDistrictDto(
    string Name,
    string Description,
    long Population,
    double AreaKm2,
    string Code,
    Guid MunicipalityId,
    Guid Version
);