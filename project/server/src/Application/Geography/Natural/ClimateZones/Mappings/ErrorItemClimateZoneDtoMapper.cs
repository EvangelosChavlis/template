// source
using server.src.Domain.Geography.Natural.ClimateZones.Dtos;

namespace server.src.Application.Geography.Natural.ClimateZones.Mappings;

/// <summary>
/// Provides a method to generate an <see cref="ItemClimateZoneDto"/> with default values  
/// to represent an error state when a valid climatezone cannot be retrieved.
/// </summary>
public class ErrorItemClimateZoneDtoMapper
{
    /// <summary>
    /// Creates an <see cref="ItemClimateZoneDto"/> with default values in case of an error.
    /// </summary>
    /// <returns>An <see cref="ItemClimateZoneDto"/> with empty or placeholder values, indicating an error state.</returns>
    public static ItemClimateZoneDto ErrorItemClimateZoneDtoMapping() => 
        new (
            Id: Guid.Empty,
            Name: string.Empty,
            Description: string.Empty,
            Code: string.Empty,
            AvgTemperatureC: double.MinValue,
            AvgPrecipitationMm: double.MinValue,
            IsActive: false,
            Version: Guid.Empty 
        );
}