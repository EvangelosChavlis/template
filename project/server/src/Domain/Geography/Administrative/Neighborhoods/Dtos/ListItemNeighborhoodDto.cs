namespace server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;

/// <summary>
/// Minimal DTO for listing neighborhoods with basic data.
/// </summary>
public record ListItemNeighborhoodDto(
    Guid Id,
    string Name,
    string Zipcode,
    string Code,
    bool IsActive,
    int Count
);