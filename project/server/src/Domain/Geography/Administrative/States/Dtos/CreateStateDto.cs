namespace server.src.Domain.Geography.Administrative.States.Dtos;

/// <summary>
/// DTO for creating a new state. 
/// Used to encapsulate the necessary data for creating a state.
/// </summary>
public record CreateStateDto(
    string Name,
    string Description,
    string Capital,
    long Population,
    double AreaKm2,
    Guid CountryId
);