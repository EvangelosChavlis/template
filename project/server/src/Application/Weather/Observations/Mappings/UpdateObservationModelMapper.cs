// source
using server.src.Domain.Weather.Observations.Dtos;
using server.src.Domain.Weather.Observations.Models;
using server.src.Domain.Weather.MoonPhases.Models;
using server.src.Domain.Geography.Natural.Locations.Models;

namespace server.src.Application.Weather.Observations.Mappings;

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
    /// <param name="locationModel">The updated location model where the observation applies.</param>
    /// <param name="moonPhaseModel">The updated moon phase model associated with the observation.</param>
    public static void UpdateObservationModelMapping(
        this UpdateObservationDto dto, 
        Observation model,
        Location locationModel, 
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
        model.LocationId = locationModel.Id;
        model.Location = locationModel;
        model.MoonPhaseId = moonPhaseModel.Id;
        model.MoonPhase = moonPhaseModel;
        model.Version = Guid.NewGuid();
    }
}