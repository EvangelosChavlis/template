namespace server.src.Domain.Geography.Administrative.Districts.Dtos;

/// <summary>
/// DTO for creating a new district. Used to encapsulate 
/// the necessary data for creating a district.
/// </summary>
public record CreateDistrictDto(
    string Name,
    string Description,
    long Population,
    double AreaKm2,
    string Code,
    Guid MunicipalityId
);
