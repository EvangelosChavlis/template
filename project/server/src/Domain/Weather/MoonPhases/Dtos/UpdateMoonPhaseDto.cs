namespace server.src.Domain.Weather.MoonPhases.Dtos;

/// <summary>
/// Represents a data transfer object for updating an existing moon phase.
/// </summary>
public record UpdateMoonPhaseDto(
    string Name,
    string Description,
    double IlluminationPercentage,
    int PhaseOrder,
    double DurationDays,
    bool IsSignificant,
    bool IsActive,
    DateTime OccurrenceDate,
    Guid Version
);