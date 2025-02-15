// source
using server.src.Domain.Geography.Locations.Models;
using server.src.Domain.Weather.Forecasts.Dtos;
using server.src.Domain.Weather.Forecasts.Models;
using server.src.Domain.Weather.MoonPhases.Models;
using server.src.Domain.Weather.Warnings.Models;

namespace server.src.Application.Weather.Forecasts.Mappings;

/// <summary>
/// Provides mapping functionality to update an existing <see cref="Forecast"/> 
/// model with new data from an <see cref="UpdateForecastDto"/> and related domain models.
/// </summary>
public static class UpdateForecastModelMapper
{
    /// <summary>
    /// Updates an existing <see cref="Forecast"/> instance with new data from an <see cref="UpdateForecastDto"/>, 
    /// along with updated warning, location, and moon phase models.
    /// </summary>
    /// <param name="dto">The data transfer object containing updated forecast details.</param>
    /// <param name="model">The existing forecast model to be modified.</param>
    /// <param name="warningModel">The updated warning model linked to the forecast.</param>
    /// <param name="locationModel">The updated location model where the forecast applies.</param>
    /// <param name="moonPhaseModel">The updated moon phase model associated with the forecast.</param>
    public static void UpdateForecastModelMapping(
        this UpdateForecastDto dto, 
        Forecast model, 
        Warning warningModel, 
        Location locationModel, 
        MoonPhase moonPhaseModel)
    {
        model.Date = dto.Date;
        model.TemperatureC = dto.TemperatureC;
        model.FeelsLikeC = dto.FeelsLikeC;
        model.Humidity = dto.Humidity;
        model.WindSpeedKph = dto.WindSpeedKph;
        model.WindDirection = dto.WindDirection;
        model.PressureHpa = dto.PressureHpa;
        model.PrecipitationMm = dto.PrecipitationMm;
        model.VisibilityKm = dto.VisibilityKm;
        model.UVIndex = dto.UVIndex;
        model.AirQualityIndex = dto.AirQualityIndex;
        model.CloudCover = dto.CloudCover;
        model.LightningProbability = dto.LightningProbability;
        model.PollenCount = dto.PollenCount;
        model.Sunrise = dto.Sunrise;
        model.Sunset = dto.Sunset;
        model.Summary = dto.Summary;
        model.LocationId = locationModel.Id;
        model.Location = locationModel;
        model.WarningId = warningModel.Id;
        model.Warning = warningModel;
        model.MoonPhaseId = moonPhaseModel.Id;
        model.MoonPhase = moonPhaseModel;
        model.Version = Guid.NewGuid();
    }
}