// source
using server.src.Domain.Weather.Collections.Observations.Dtos;
using server.src.Domain.Weather.Collections.Observations.Models;
using server.src.Domain.Weather.Collections.MoonPhases.Models;
using server.src.Domain.Geography.Administrative.Stations.Models;

namespace server.src.Application.Weather.Collections.Observations.Mappings;

/// <summary>
/// Provides mapping functionality to update an existing <see cref="Observation"/> 
/// model with new data from an <see cref="UpdateObservationDto"/> and related domain models.
/// </summary>
public static class UpdateObservationModelMapper
{
    /// <summary>
    /// Updates an existing <see cref="Observation"/> instance with new data from an <see cref="UpdateObservationDto"/>, 
    /// along with updated warning, location, and moon phase models.
    /// </summary>
    /// <param name="dto">The data transfer object containing updated observation details.</param>
    /// <param name="model">The existing observation model to be modified.</param>
    /// <param name="warningModel">The updated warning model linked to the observation.</param>
    /// <param name="stationModel">The updated station model where the observation applies.</param>
    /// <param name="moonPhaseModel">The updated moon phase model associated with the observation.</param>
    public static void UpdateObservationModelMapping(
        this UpdateObservationDto dto, 
        Observation model,
        Station stationModel, 
        MoonPhase moonPhaseModel)
    {
        model.Timestamp = dto.Timestamp;
        model.TemperatureC = dto.TemperatureC;
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
        model.StationId = stationModel.Id;
        model.Station = stationModel;
        model.MoonPhaseId = moonPhaseModel.Id;
        model.MoonPhase = moonPhaseModel;
        model.Version = Guid.NewGuid();
    }
}