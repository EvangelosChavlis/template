namespace server.src.Domain.Weather.Collections.MoonPhases.Dtos;

/// <summary>
/// Represents a detailed moon phase item, including its unique identifier.
/// </summary>
public record ItemMoonPhaseDto(
    Guid Id,
    string Name, 
    string Description,
    string Code,
    double IlluminationPercentage,
    int PhaseOrder,
    double DurationDays,
    bool IsSignificant,
    bool IsActive,
    DateTime OccurrenceDate,
    Guid Version
);