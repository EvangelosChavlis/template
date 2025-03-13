namespace server.src.Domain.Geography.Administrative.Municipalities.Dtos;

/// <summary>
/// DTO for importing a new municipality. Used to encapsulate the 
/// necessary data for importing a municipality.
/// </summary>
public record ImportMunicipalityDto(
    string Name,
    string Description,
    double AreaKm2,
    long Population,
    string Code
);