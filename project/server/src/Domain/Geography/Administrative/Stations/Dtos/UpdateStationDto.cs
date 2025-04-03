namespace server.src.Domain.Geography.Administrative.Stations.Dtos;

/// <summary>
/// DTO for updating an existing station. Used to encapsulate the data for updating a station's details.
/// </summary>
public record UpdateStationDto(
    string Name,
    string Description,
    string Code,
    double Latitude,
    double Longitude,
    bool IsActive,
    Guid LocationId,
    Guid NeighborhoodId,
    Guid Version
);