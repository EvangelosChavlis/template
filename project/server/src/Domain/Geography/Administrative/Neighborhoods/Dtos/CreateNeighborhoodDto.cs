namespace server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;

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