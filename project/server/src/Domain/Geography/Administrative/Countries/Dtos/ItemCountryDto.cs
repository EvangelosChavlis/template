namespace server.src.Domain.Geography.Administrative.Countries.Dtos;

/// <summary>
/// DTO for a detailed view of a country. Includes all 
/// relevant details about a country.
/// </summary>
public record ItemCountryDto(
    Guid Id,
    string Name,
    string Description,
    string Code,
    string Capital,
    long Population,
    double AreaKm2,
    string PhoneCode,
    string TLD,
    string Currency,
    bool IsActive,
    string Continent,
    Guid Version
);
