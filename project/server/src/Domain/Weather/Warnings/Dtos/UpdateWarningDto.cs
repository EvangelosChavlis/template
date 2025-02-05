namespace server.src.Domain.Weather.Warnings.Dtos;

/// <summary>
/// Represents a warning data transfer object containing the name 
/// and description of the warning.
/// </summary>
public record UpdateWarningDto(
    string Name,
    string Description,
    string RecommendedActions,
    Guid Version
);
