namespace server.src.Domain.Weather.Collections.Warnings.Dtos;

/// <summary>
/// Represents a simplified warning item that contains basic details 
/// about the warning, including the number of related forecasts.
/// </summary>
public record ListItemWarningDto(
    Guid Id, 
    string Name, 
    string Code,
    int Count
);
