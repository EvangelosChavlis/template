namespace server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;

/// <summary>
/// DTO for retrieving detailed Neighborhood data.
/// </summary>
public record ItemNeighborhoodDto(
    Guid Id,
    string Name,
    string Description,
    long Population,
    double AreaKm2,
    string Zipcode,
    string Code,
    bool IsActive,
    string District,
    Guid Version
);