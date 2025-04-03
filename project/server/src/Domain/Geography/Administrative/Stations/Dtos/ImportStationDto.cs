namespace server.src.Domain.Geography.Administrative.Stations.Dtos;

/// <summary>
/// DTO for importing a new station. 
/// Used to encapsulate the necessary data for importing a station.
/// </summary>
public record ImportStationDto(
    string Name,
    string Description,
    string Code,
    double Latitude,
    double Longitude,
    string NeighborhoodCode
);