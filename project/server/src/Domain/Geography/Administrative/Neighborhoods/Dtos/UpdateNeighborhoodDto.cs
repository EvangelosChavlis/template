namespace server.src.Domain.Geography.Natural.Neighborhoods.Dtos;

/// <summary>
/// DTO for updating an existing Neighborhood.
/// </summary>
public record UpdateNeighborhoodDto(
    string Name,
    string Description,
    long Population,
    string Zipcode,
    Guid Version
);
