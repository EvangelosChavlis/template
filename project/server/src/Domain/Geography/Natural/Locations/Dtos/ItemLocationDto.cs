namespace server.src.Domain.Geography.Natural.Locations.Dtos;

/// <summary>
/// Represents the data transfer object (DTO) for a location item.  
/// Contains geographic coordinates, status, and associated environmental details.  
/// Used for retrieving location data with relevant attributes such as timezone, terrain type, and climate zone.
/// </summary>
public record ItemLocationDto(
    double Longitude,
    double Latitude,
    double Altitude,
    bool IsActive,
    string Timezone,
    string TerrainType,
    string ClimateZone,
    Guid Version
);