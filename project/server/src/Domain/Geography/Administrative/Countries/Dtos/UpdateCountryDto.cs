namespace server.src.Domain.Geography.Administrative.Countries.Dtos;

/// <summary>
/// DTO for updating an existing country. Used to encapsulate 
/// the data for updating a country's details.
/// </summary>
public record UpdateCountryDto(
    string Name,
    string Description,
    string IsoCode,
    string Capital,
    long Population,
    double AreaKm2,
    Guid ContinentId,
    Guid Version
);