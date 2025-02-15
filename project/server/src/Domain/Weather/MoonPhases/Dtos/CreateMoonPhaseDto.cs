namespace server.src.Domain.Weather.MoonPhases.Dtos;

/// <summary>
/// Represents a data transfer object for creating a new moon phase.
/// </summary>
public record CreateMoonPhaseDto(
    string Name,
    string Description,
    double IlluminationPercentage,
    int PhaseOrder,
    double DurationDays,
    bool IsSignificant,
    bool IsActive,
    DateTime OccurrenceDate
);