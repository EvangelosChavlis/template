namespace server.src.Domain.Geography.Administrative.Countries.Dtos;

/// <summary>
/// DTO for a detailed view of a country. Includes all 
/// relevant details about a country.
/// </summary>
public record ItemCountryDto(
    Guid Id,
    string Name,
    string Description,
    string IsoCode,
    string Capital,
    long Population,
    double AreaKm2,
    bool IsActive,
    string Continent
);
