namespace server.src.Domain.Geography.Administrative.Continents.Dtos;

/// <summary>
/// DTO for updating an existing continent. This encapsulates the data needed
/// to modify an existing continent, including the version for concurrency control.
/// </summary>
public record UpdateContinentDto(
    string Name,
    string Code,
    string Description,
    Guid Version
);