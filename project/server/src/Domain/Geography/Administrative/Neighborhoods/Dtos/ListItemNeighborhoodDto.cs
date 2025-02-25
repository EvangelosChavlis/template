namespace server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;

/// <summary>
/// Minimal DTO for listing neighborhoods with basic data.
/// </summary>
/// <param name="Id">The unique identifier of the neighborhood.</param>
/// <param name="Name">The name of the neighborhood.</param>
/// <param name="Zipcode">The ZIP code of the neighborhood.</param>
public record ListItemNeighborhoodDto(
    Guid Id,
    string Name,
    string Zipcode,
    bool IsActive,
    int Count
);
