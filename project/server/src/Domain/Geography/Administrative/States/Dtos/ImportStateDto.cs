namespace server.src.Domain.Geography.Administrative.States.Dtos;

/// <summary>
/// DTO for importing a new state. 
/// Used to encapsulate the necessary data for importing a state.
/// </summary>
public record ImportStateDto(
    string Name,
    string Description,
    string Capital,
    long Population,
    string Code,
    double AreaKm2
);