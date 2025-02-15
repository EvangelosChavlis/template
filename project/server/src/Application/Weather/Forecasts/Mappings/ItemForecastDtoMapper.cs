// source
using server.src.Domain.Common.Extensions;
using server.src.Domain.Weather.Forecasts.Dtos;
using server.src.Domain.Weather.Forecasts.Models;

namespace server.src.Application.Weather.Forecasts.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Forecast"/> model 
/// into an <see cref="ItemForecastDto"/> for detailed individual item representation.
/// </summary>
public static class ItemForecastDtoMapper
{
    /// <summary>
    /// Converts a <see cref="Forecast"/> model into an <see cref="ItemForecastDto"/>, 
    /// capturing all relevant details for an individual forecast view.
/// </summary>
    /// <param name="model">The forecast model to be mapped.</param>
    /// <returns>A fully populated <see cref="ItemForecastDto"/> representing the forecast data.</returns>
    public static ItemForecastDto ItemForecastDtoMapping(this Forecast model) 
        => new(
            Id: model.Id,
            Date: model.Date.GetFullLocalDateTimeString(),
            TemperatureC: model.TemperatureC,
            TemperatureF: model.TemperatureF,
            FeelsLikeC: model.FeelsLikeC,
            Humidity: model.Humidity,
            WindSpeedKph: model.WindSpeedKph,
            WindDirection: model.WindDirection,
            PressureHpa: model.PressureHpa,
            PrecipitationMm: model.PrecipitationMm,
            VisibilityKm: model.VisibilityKm,
            UVIndex: model.UVIndex,
            AirQualityIndex: model.AirQualityIndex,
            CloudCover: model.CloudCover,
            LightningProbability: model.LightningProbability,
            PollenCount: model.PollenCount,
            Sunrise: model.Sunrise,
            Sunset: model.Sunset,
            Longitude: model.Location.Longitude,
            Latitude: model.Location.Latitude,
            Altitude: model.Location.Altitude,
            Summary: model.Summary,
            Warning: model.Warning.Name,
            MoonPhase: model.MoonPhase.Name,
            Location: $"({model.Location.Latitude}, {model.Location.Longitude}, {model.Location.Altitude})",
            Version: model.Version
        );
}
