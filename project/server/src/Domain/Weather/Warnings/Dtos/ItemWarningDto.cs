namespace server.src.Domain.Weather.Warnings.Dtos;

/// <summary>
/// Represents a detailed warning item, including its unique identifier,  
/// name, description, recommended actions, and version information.
/// </summary>
public record ItemWarningDto(
    Guid Id,
    string Name, 
    string Description,
    string RecommendedActions,
    Guid Version
);