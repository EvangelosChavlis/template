namespace server.src.Domain.Geography.Administrative.Countries.Dtos;

/// <summary>
/// DTO for listing countries. Provides a summarized 
/// view of a country for display in lists.
/// </summary>
public record ListItemCountryDto(
    Guid Id,
    string Name,
    string IsoCode,
    bool IsActive,
    long Population
);