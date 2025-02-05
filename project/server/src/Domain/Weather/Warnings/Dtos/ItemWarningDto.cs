namespace server.src.Domain.Weather.Warnings.Dtos;

/// <summary>
/// Represents a detailed warning item that includes the warning's name, 
/// description, and a list of related forecasts.
/// </summary>
public record ItemWarningDto(
    Guid Id,
    string Name, 
    string Description,
    Guid Version
);