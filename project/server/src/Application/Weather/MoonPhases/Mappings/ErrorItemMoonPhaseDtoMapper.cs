// source
using server.src.Domain.Weather.MoonPhases.Dtos;

namespace server.src.Application.Weather.MoonPhases.Mappings;

/// <summary>
/// Provides a method to generate an <see cref="ItemMoonPhaseDto"/> with default values  
/// to represent an error state when a valid moon phase cannot be retrieved.
/// </summary>
public class ErrorItemMoonPhaseDtoMapper
{
    /// <summary>
    /// Creates an <see cref="ItemMoonPhaseDto"/> with default values in case of an error.
    /// </summary>
    /// <returns>An <see cref="ItemMoonPhaseDto"/> with empty or placeholder values, indicating an error state.</returns>
    public static ItemMoonPhaseDto ErrorItemMoonPhaseDtoMapping() => 
        new(
            Id: Guid.Empty,
            Name: string.Empty,
            Description: string.Empty,
            IlluminationPercentage: 0,
            PhaseOrder: 0,
            DurationDays: 0,
            IsSignificant: false,
            IsActive: false,
            OccurrenceDate: DateTime.MinValue,
            Version: Guid.Empty
        );
}