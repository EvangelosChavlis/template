namespace server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;

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
    string District,
    Guid Version
);