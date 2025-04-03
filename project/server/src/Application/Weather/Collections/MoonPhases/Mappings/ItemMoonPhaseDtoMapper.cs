// source
using server.src.Domain.Weather.Collections.MoonPhases.Dtos;
using server.src.Domain.Weather.Collections.MoonPhases.Models;

namespace server.src.Application.Weather.Collections.MoonPhases.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="MoonPhase"/> model  
/// into an <see cref="ItemMoonPhaseDto"/> for detailed item representation.
/// </summary>
public static class ItemMoonPhaseDtoMapper
{
    /// <summary>
    /// Maps a <see cref="MoonPhase"/> model to an <see cref="ItemMoonPhaseDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="MoonPhase"/> model that will be mapped to an <see cref="ItemMoonPhaseDto"/>.</param>
    /// <returns>An <see cref="ItemMoonPhaseDto"/> representing the MoonPhase model with full details for an individual item view.</returns>
    public static ItemMoonPhaseDto ItemMoonPhaseDtoMapping(this MoonPhase model) =>
        new(
            Id: model.Id,
            Name: model.Name,
            Description: model.Description,
            Code: model.Code,
            IlluminationPercentage: model.IlluminationPercentage,
            PhaseOrder: model.PhaseOrder,
            DurationDays: model.DurationDays,
            IsSignificant: model.IsSignificant,
            IsActive: model.IsActive,
            OccurrenceDate: model.OccurrenceDate,
            Version: model.Version
        );
}