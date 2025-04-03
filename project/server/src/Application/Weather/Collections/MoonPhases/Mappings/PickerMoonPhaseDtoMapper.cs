// source
using server.src.Domain.Weather.Collections.MoonPhases.Dtos;
using server.src.Domain.Weather.Collections.MoonPhases.Models;

namespace server.src.Application.Weather.Collections.MoonPhases.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="MoonPhase"/> model 
/// into a <see cref="PickerMoonPhaseDto"/>.
/// This mapper is used to transform moonphase data for selection lists or dropdowns.
/// </summary>
public static class PickerMoonPhaseDtoMapper
{
    /// <summary>
    /// Maps a <see cref="MoonPhase"/> model to a <see cref="PickerMoonPhaseDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="MoonPhase"/> model that will be mapped.</param>
    /// <returns>A <see cref="PickerMoonPhaseDto"/> containing essential details for selection purposes.</returns>
    public static PickerMoonPhaseDto PickerMoonPhaseDtoMapping(
        this MoonPhase model) => new(
            Id: model.Id,
            Name: model.Name
        );
}
