// source
using server.src.Domain.Common.Extensions;
using server.src.Domain.Weather.Collections.Observations.Dtos;
using server.src.Domain.Weather.Collections.Observations.Models;

namespace server.src.Application.Weather.Collections.Observations.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Observation"/> model 
/// into a simplified <see cref="StatItemObservationDto"/> containing key statistical data.
/// </summary>
public static class StatItemObservationDtoMapper
{
    /// <summary>
    /// Maps a <see cref="Observation"/> model to a <see cref="StatItemObservationDto"/>, 
    /// extracting the observation's ID, formatted date, and temperature for statistical purposes.
    /// </summary>
    /// <param name="model">The observation model to be mapped.</param>
    /// <returns>A <see cref="StatItemObservationDto"/> containing the observation's ID, formatted date, and temperature.</returns>
    public static StatItemObservationDto StatItemObservationDtoMapping(
        this Observation model) => new(
            Id: model.Id,
            Timestamp: model.Timestamp.GetFullLocalDateTimeString(),
            TemperatureC: model.TemperatureC
        );
}