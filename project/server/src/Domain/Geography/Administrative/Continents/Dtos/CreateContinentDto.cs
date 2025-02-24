namespace server.src.Domain.Geography.Administrative.Continents.Dtos;

/// <summary>
/// DTO for creating a new continent. This is used to 
/// encapsulate data for continent creation.
/// </summary>
public record CreateContinentDto(
    string Name,
    string Description
);