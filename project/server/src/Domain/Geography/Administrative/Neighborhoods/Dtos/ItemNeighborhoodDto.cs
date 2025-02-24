namespace server.src.Domain.Geography.Natural.Neighborhoods.Dtos;

/// <summary>
/// DTO for retrieving detailed Neighborhood data.
/// </summary>
public record ItemNeighborhoodDto(
    Guid Id,
    string Name,
    string Description,
    long Population,
    string Zipcode,
    bool IsActive,
    Guid Vesrion,
    string District
);