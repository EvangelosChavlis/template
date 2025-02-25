namespace server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;

/// <summary>
/// DTO for updating an existing Neighborhood.
/// </summary>
public record UpdateNeighborhoodDto(
    string Name,
    string Description,
    long Population,
    string Zipcode,
    Guid DistrictId,
    Guid Version
);
