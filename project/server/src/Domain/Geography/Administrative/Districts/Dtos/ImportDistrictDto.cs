namespace server.src.Domain.Geography.Administrative.Districts.Dtos;

/// <summary>
/// DTO for importing a new district. Used to encapsulate 
/// the necessary data for importing a district.
/// </summary>
public record ImportDistrictDto(
    string Name,
    string Description,
    long Population,
    double AreaKm2,
    string Code
);