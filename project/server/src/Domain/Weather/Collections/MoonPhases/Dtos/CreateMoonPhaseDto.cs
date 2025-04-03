namespace server.src.Domain.Weather.Collections.MoonPhases.Dtos;

/// <summary>
/// Represents a data transfer object for creating a new moon phase.
/// </summary>
public record CreateMoonPhaseDto(
    string Name,
    string Description,
    string Code,
    double IlluminationPercentage,
    int PhaseOrder,
    double DurationDays,
    bool IsSignificant,
    bool IsActive,
    DateTime OccurrenceDate
);