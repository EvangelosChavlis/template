// source
using server.src.Domain.Weather.Collections.MoonPhases.Dtos;
using server.src.Domain.Weather.Collections.MoonPhases.Models;

namespace server.src.Application.Weather.Collections.MoonPhases.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="MoonPhase"/> model 
/// into a <see cref="ListItemMoonPhaseDto"/>.
/// This utility class is used to transform moonphase data for list views with key details.
/// </summary>
public static class ListItemMoonPhaseDtoMapper
{
    /// <summary>
    /// Maps a <see cref="MoonPhase"/> model to a <see cref="ListItemMoonPhaseDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="MoonPhase"/> model that will be mapped.</param>
    /// <returns>A <see cref="ListItemMoonPhaseDto"/> representing the moonphase model with essential details.</returns>
    public static ListItemMoonPhaseDto ListItemMoonPhaseDtoMapping(
        this MoonPhase model) => new(
            Id: model.Id,
            Name: model.Name,
            Code: model.Code,
            ForecastCount: model.Forecasts.Count,
            ObservationCount: model.Observations.Count
        );
}