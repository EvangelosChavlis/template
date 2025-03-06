namespace server.src.Domain.Geography.Administrative.Countries.Dtos;

/// <summary>
/// DTO for importing country data. Used to encapsulate 
/// the necessary information for importing a country into the system.
/// </summary>
public record ImportCountryDto(
    string Name,
    string Description,
    string Code,
    string Capital,
    long Population,
    double AreaKm2,
    bool IsActive
);