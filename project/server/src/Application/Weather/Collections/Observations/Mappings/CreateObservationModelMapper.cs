// source
using server.src.Domain.Weather.Collections.Observations.Dtos;
using server.src.Domain.Weather.Collections.Observations.Models;
using server.src.Domain.Weather.Collections.MoonPhases.Models;
using server.src.Domain.Geography.Natural.Locations.Models;
using server.src.Domain.Geography.Administrative.Stations.Models;

namespace server.src.Application.Weather.Collections.Observations.Mappings;

/// <summary>
/// Provides mapping functionality to create a <see cref="Observation"/> model 
/// from a <see cref="CreateObservationDto"/> and related domain models.
/// </summary>
public static class CreateObservationModelMapper
{
    /// <summary>
    /// Creates a new instance of <see cref="Observation"/> using data from a <see cref="CreateObservationDto"/>, 
    /// along with associated warning, location, and moon phase models.
    /// </summary>
    /// <param name="dto">The data transfer object containing observation details.</param>
    /// <param name="warningModel">The warning model associated with the observation.</param>
    /// <param name="stationModel">The station model where the observation applies.</param>
    /// <param name="moonPhase">The moon phase model linked to the observation.</param>
    /// <returns>A fully populated <see cref="Observation"/> instance.</returns>
    public static Observation CreateObservationModelMapping(
        this CreateObservationDto dto, 
        Station stationModel, 
        MoonPhase moonPhase)
        => new()
        {
            Timestamp = dto.Timestamp,
            TemperatureC = dto.TemperatureC,
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
            StationId = stationModel.Id,
            Station = stationModel,
            MoonPhaseId = moonPhase.Id,
            MoonPhase = moonPhase,
            Version = Guid.NewGuid()
        };
}
