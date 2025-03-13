namespace server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;

/// <summary>
/// DTO for importing a new Neighborhood.
/// </summary>
public record ImportNeighborhoodDto(
    string Name,
    string Description,
    long Population,
    double AreaKm2,
    string Zipcode,
    string Code
);