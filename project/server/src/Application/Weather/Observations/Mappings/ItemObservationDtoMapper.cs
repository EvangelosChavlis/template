// source
using server.src.Domain.Common.Extensions;
using server.src.Domain.Weather.Observations.Dtos;
using server.src.Domain.Weather.Observations.Models;

namespace server.src.Application.Weather.Observations.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Observation"/> model 
/// into an <see cref="ItemObservationDto"/> for detailed individual item representation.
/// </summary>
public static class ItemObservationDtoMapper
{
    /// <summary>
    /// Converts a <see cref="Observation"/> model into an <see cref="ItemObservationDto"/>, 
    /// capturing all relevant details for an individual observation view.
/// </summary>
    /// <param name="model">The observation model to be mapped.</param>
    /// <returns>A fully populated <see cref="ItemObservationDto"/> representing the observation data.</returns>
    public static ItemObservationDto ItemObservationDtoMapping(this Observation model) 
        => new(
            Id: model.Id,
            Timestamp: model.Timestamp.GetFullLocalDateTimeString(),
            TemperatureC: model.TemperatureC,
            TemperatureF: model.TemperatureF,
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
            Longitude: model.Location.Longitude,
            Latitude: model.Location.Latitude,
            Altitude: model.Location.Altitude,
            MoonPhase: model.MoonPhase.Name,
            Location: @$"({model.Location.Latitude}, 
                {model.Location.Longitude}, {model.Location.Altitude})",
            Version: model.Version
        );
}
