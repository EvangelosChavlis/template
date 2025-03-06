namespace server.src.Domain.Geography.Administrative.Continents.Dtos;

/// <summary>
/// DTO for representing a detailed view of a continent.
/// This is used to display comprehensive information about a continent,
/// including its name, description, and active status.
/// </summary>
public record ItemContinentDto(
    Guid Id,
    string Name,
    string Code,
    string Description,
    bool IsActive,
    Guid Version
);