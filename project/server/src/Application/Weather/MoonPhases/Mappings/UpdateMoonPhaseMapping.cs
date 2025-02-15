// source
using server.src.Domain.Weather.MoonPhases.Dtos;
using server.src.Domain.Weather.MoonPhases.Models;

namespace server.src.Application.Weather.MoonPhases.Mappings;

/// <summary>
/// Provides mapping functionality to update an existing <see cref="MoonPhase"/> model 
/// using data from an <see cref="UpdateMoonPhaseDto"/>.
/// This utility class ensures that the MoonPhase entity is updated efficiently with new details.
/// </summary>
public static class UpdateMoonPhaseMapper
{
    /// <summary>
    /// Updates an existing <see cref="MoonPhase"/> model with data from an <see cref="UpdateMoonPhaseDto"/>.
    /// </summary>
    /// <param name="dto">The data transfer object containing updated MoonPhase details.</param>
    /// <param name="model">The existing <see cref="MoonPhase"/> model to be updated.</param>
    public static void UpdateMoonPhaseMapping(this UpdateMoonPhaseDto dto, MoonPhase model)
    {
        model.Name = dto.Name;
        model.Description = dto.Description;
        model.IlluminationPercentage = dto.IlluminationPercentage;
        model.PhaseOrder = dto.PhaseOrder;
        model.DurationDays = dto.DurationDays;
        model.IsSignificant = dto.IsSignificant;
        model.IsActive = dto.IsActive;
        model.OccurrenceDate = dto.OccurrenceDate;
        model.Version = Guid.NewGuid();
    }
}