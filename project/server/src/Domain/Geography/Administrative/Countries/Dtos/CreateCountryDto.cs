namespace server.src.Domain.Geography.Administrative.Countries.Dtos;

/// <summary>
/// DTO for creating a new country. Used to encapsulate 
/// the necessary data for creating a country.
/// </summary>
public record CreateCountryDto(
    string Name,
    string Description,
    string IsoCode,
    string Capital,
    long Population,
    double AreaKm2,
    bool IsActive,
    Guid ContinentId
);