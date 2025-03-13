namespace server.src.Domain.Geography.Administrative.Countries.Dtos;

/// <summary>
/// DTO for creating a new country. Used to encapsulate 
/// the necessary data for creating a country.
/// </summary>
public record CreateCountryDto(
    string Name,
    string Description,
    string Code,
    string Capital,
    long Population,
    double AreaKm2,
    string PhoneCode,
    string TLD,
    string Currency,
    Guid ContinentId
);