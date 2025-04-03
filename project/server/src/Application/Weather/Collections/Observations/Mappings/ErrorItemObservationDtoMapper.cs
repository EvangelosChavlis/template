// source
using server.src.Domain.Weather.Collections.Observations.Dtos;

namespace server.src.Application.Weather.Collections.Observations.Mappings;

/// <summary>
/// Provides a method to generate an <see cref="ItemObservationDto"/> with default values 
/// to represent an error state when a valid observation cannot be retrieved.
/// </summary>
public static class ErrorItemObservationDtoMapper
{
    /// <summary>
    /// Creates an <see cref="ItemObservationDto"/> with default or placeholder values, 
    /// typically used to indicate an error scenario or missing observation data.
    /// </summary>
    /// <returns>An <see cref="ItemObservationDto"/> populated with default error-state values.</returns>
    public static ItemObservationDto ErrorItemObservationDtoMapping() 
        => new (
            Id: Guid.Empty,
            Timestamp: string.Empty,
            TemperatureC: int.MinValue,
            TemperatureF: int.MinValue,
            Humidity: int.MinValue,
            WindSpeedKph: double.MinValue,
            WindDirection: int.MinValue,
            PressureHpa: double.MinValue,
            PrecipitationMm: double.MinValue,
            VisibilityKm: double.MinValue,
            UVIndex: int.MinValue,
            AirQualityIndex: int.MinValue,
            CloudCover: int.MinValue,
            LightningProbability: int.MinValue,
            PollenCount: int.MinValue,
            MoonPhase: string.Empty,
            Station: string.Empty,
            Version: Guid.Empty
        );
}
