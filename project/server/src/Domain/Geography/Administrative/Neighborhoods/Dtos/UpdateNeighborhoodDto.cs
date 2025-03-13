namespace server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;

/// <summary>
/// DTO for updating an existing Neighborhood.
/// </summary>
public record UpdateNeighborhoodDto(
    string Name,
    string Description,
    long Population,
    long AreaKm2,
    string Zipcode,
    string Code,
    Guid DistrictId,
    Guid Version
);
