// source
using server.src.Domain.Geography.Natural.Locations.Models;
using server.src.Domain.Weather.Forecasts.Dtos;
using server.src.Domain.Weather.Forecasts.Models;
using server.src.Domain.Weather.MoonPhases.Models;
using server.src.Domain.Weather.Warnings.Models;

namespace server.src.Application.Weather.Forecasts.Mappings;

/// <summary>
/// Provides mapping functionality to create a <see cref="Forecast"/> model 
/// from a <see cref="CreateForecastDto"/> and related domain models.
/// </summary>
public static class CreateForecastModelMapper
{
    /// <summary>
    /// Creates a new instance of <see cref="Forecast"/> using data from a <see cref="CreateForecastDto"/>, 
    /// along with associated warning, location, and moon phase models.
    /// </summary>
    /// <param name="dto">The data transfer object containing forecast details.</param>
    /// <param name="warningModel">The warning model associated with the forecast.</param>
    /// <param name="locationModel">The location model where the forecast applies.</param>
    /// <param name="moonPhase">The moon phase model linked to the forecast.</param>
    /// <returns>A fully populated <see cref="Forecast"/> instance.</returns>
    public static Forecast CreateForecastModelMapping(
        this CreateForecastDto dto, 
        Warning warningModel, 
        Location locationModel, 
        MoonPhase moonPhase)
        => new()
        {
            Date = dto.Date,
            TemperatureC = dto.TemperatureC,
            FeelsLikeC = dto.FeelsLikeC,
            Humidity = dto.Humidity,
            WindSpeedKph = dto.WindSpeedKph,
            WindDirection = dto.WindDirection,
            PressureHpa = dto.PressureHpa,
            PrecipitationMm = dto.PrecipitationMm,
            VisibilityKm = dto.VisibilityKm,
            UVIndex = dto.UVIndex,
            AirQualityIndex = dto.AirQualityIndex,
            CloudCover = dto.CloudCover,
            LightningProbability = dto.LightningProbability,
            PollenCount = dto.PollenCount,
            Sunrise = dto.Sunrise,
            Sunset = dto.Sunset,
            Summary = dto.Summary,
            WarningId = warningModel.Id,
            Warning = warningModel,
            LocationId = locationModel.Id,
            Location = locationModel,
            MoonPhaseId = moonPhase.Id,
            MoonPhase = moonPhase,
            Version = Guid.NewGuid()
        };
}
