namespace server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;

/// <summary>
/// DTO for creating a new Neighborhood.
/// </summary>
public record CreateNeighborhoodDto(
    string Name,
    string Description,
    long Population,
    double AreaKm2,
    string Zipcode,
    string Code,
    Guid DistrictId
);