// source
using server.src.Domain.Weather.Collections.MoonPhases.Dtos;
using server.src.Domain.Weather.Collections.MoonPhases.Models;

namespace server.src.Application.Weather.Collections.MoonPhases.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="CreateMoonPhaseDto"/> into a <see cref="MoonPhase"/> model.
/// This utility class is used to create new moon phase instances based on provided data transfer objects.
/// </summary>
public static class CreateMoonPhaseModelMapper
{
    /// <summary>
    /// Maps a <see cref="CreateMoonPhaseDto"/> to a <see cref="MoonPhase"/> model, creating a new moon phase entity.
    /// </summary>
    /// <param name="dto">The data transfer object containing moon phase details.</param>
    /// <returns>A newly created <see cref="MoonPhase"/> model populated with data from the provided DTO.</returns>
    public static MoonPhase CreateMoonPhaseModelMapping(this CreateMoonPhaseDto dto)
        => new()
        {
            Name = dto.Name,
            Description = dto.Description,
            Code = dto.Code,
            IlluminationPercentage = dto.IlluminationPercentage,
            PhaseOrder = dto.PhaseOrder,
            DurationDays = dto.DurationDays,
            IsSignificant = dto.IsSignificant,
            IsActive = dto.IsActive,
            OccurrenceDate = dto.OccurrenceDate,
            Version = Guid.NewGuid()
        };
}
