namespace server.src.Domain.Geography.Administrative.Countries.Dtos;

/// <summary>
/// DTO for updating an existing country. Used to encapsulate 
/// the data for updating a country s details.
/// </summary>
public record UpdateCountryDto(
    string Name,
    string Description,
    string Code,
    string Capital,
    long Population,
    double AreaKm2,
    string PhoneCode,
    string TLD,
    string Currency,
    Guid ContinentId,
    Guid Version
);