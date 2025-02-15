// source
using server.src.Domain.Weather.Forecasts.Dtos;

namespace server.src.Application.Weather.Forecasts.Mappings;

/// <summary>
/// Provides a method to generate an <see cref="ItemForecastDto"/> with default values 
/// to represent an error state when a valid forecast cannot be retrieved.
/// </summary>
public static class ErrorItemForecastDtoMapper
{
    /// <summary>
    /// Creates an <see cref="ItemForecastDto"/> with default or placeholder values, 
    /// typically used to indicate an error scenario or missing forecast data.
    /// </summary>
    /// <returns>An <see cref="ItemForecastDto"/> populated with default error-state values.</returns>
    public static ItemForecastDto ErrorItemForecastDtoMapping() 
        => new (
            Id: Guid.Empty,
            Date: string.Empty,
            TemperatureC: int.MinValue,
            TemperatureF: int.MinValue,
            FeelsLikeC: int.MinValue,
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
            Sunrise: TimeSpan.MinValue,
            Sunset: TimeSpan.MinValue,
            Longitude: double.MinValue,
            Latitude: double.MinValue,
            Altitude: double.MinValue,
            Summary: string.Empty,
            Warning: string.Empty,
            MoonPhase: string.Empty,
            Location: string.Empty,
            Version: Guid.Empty
        );
}
