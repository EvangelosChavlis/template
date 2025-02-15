namespace server.src.Domain.Weather.MoonPhases.Dtos;

/// <summary>
/// Represents a moon phase item used in a picker, containing only the ID and name.
/// </summary>
public record PickerMoonPhaseDto(
    Guid Id, 
    string Name
);