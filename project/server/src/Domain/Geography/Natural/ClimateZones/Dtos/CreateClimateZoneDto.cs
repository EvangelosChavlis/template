namespace server.src.Domain.Geography.Natural.ClimateZones.Dtos;

/// <summary>
/// Represents the data transfer object (DTO) for creating a new climate zone.  
/// Contains necessary details such as name, description, average temperature, and precipitation.
/// </summary>
public record CreateClimateZoneDto(
    string Name,
    string Description,
    double AvgTemperatureC,
    double AvgPrecipitationMm
);