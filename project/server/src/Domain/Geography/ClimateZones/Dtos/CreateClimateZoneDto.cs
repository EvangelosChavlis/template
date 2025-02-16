namespace server.src.Domain.Geography.ClimateZones.Dtos;

public record CreateClimateZoneDto(
    string Name,
    string Description,
    double AvgTemperatureC,
    double AvgPrecipitationMm
);