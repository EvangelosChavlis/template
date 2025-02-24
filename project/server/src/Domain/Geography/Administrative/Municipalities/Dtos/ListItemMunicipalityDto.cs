namespace server.src.Domain.Geography.Administrative.Municipalities.Dtos;

/// <summary>
/// DTO for listing municipalities. Provides a summarized 
/// view of a municipality for display in lists, including name, status, and population.
/// </summary>
public record ListItemMunicipalityDto(
    Guid Id,
    string Name,
    bool IsActive,
    long Population
);