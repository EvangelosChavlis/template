namespace server.src.Domain.Geography.Administrative.Districts.Dtos;

/// <summary>
/// DTO for updating an existing district. Used to encapsulate 
/// the data for updating a district's details.
/// </summary>
public record UpdateDistrictDto(
    string Name,
    string Description,
    long Population,
    Guid MunicipalityId,
    Guid Version
);