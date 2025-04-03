namespace server.src.Domain.Weather.Collections.Warnings.Dtos;

/// <summary>
/// Represents a warning item used in a picker, containing only the warning's
/// ID and name for selection purposes.
/// </summary>
public record PickerWarningDto(
    Guid Id, 
    string Name
);