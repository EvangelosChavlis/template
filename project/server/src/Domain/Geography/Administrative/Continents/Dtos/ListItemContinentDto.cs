namespace server.src.Domain.Geography.Administrative.Continents.Dtos;

/// <summary>
/// DTO for representing a summarized list item of a continent.
/// This is used to display basic information about a continent, including its active status
/// and the count of related entities, such as countries or regions.
/// </summary>
public record ListItemContinentDto(
    Guid Id,
    string Name,
    bool IsActive,
    int Count
);