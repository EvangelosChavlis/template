namespace server.src.Domain.Geography.Natural.Neighborhoods.Dtos;

/// <summary>
/// DTO for creating a new Neighborhood.
/// </summary>
public record CreateNeighborhoodDto(
    string Name,
    string Description,
    long Population,
    string Zipcode,
    bool IsActive,
    Guid DistrictId
);