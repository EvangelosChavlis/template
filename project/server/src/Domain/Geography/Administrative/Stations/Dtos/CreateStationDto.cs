namespace server.src.Domain.Geography.Administrative.Stations.Dtos;

/// <summary>
/// DTO for creating a new station. 
/// Used to encapsulate the necessary data for creating a station.
/// </summary>
public record CreateStationDto(
    string Name,
    string Description,
    string Code,
    double Latitude,
    double Longitude,
    Guid? NeighborhoodId
);