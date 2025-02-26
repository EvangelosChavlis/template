namespace server.src.Domain.Geography.Administrative.Districts.Dtos;

/// <summary>
/// DTO for listing districts. Provides a summarized 
/// view of a district for display in lists, including name, status, and population.
/// </summary>
public record ListItemDistrictDto(
    Guid Id,
    string Name,
    long Population,
    bool IsActive,
    int Count
);