namespace server.src.Domain.Geography.Administrative.States.Dtos;

/// <summary>
/// DTO for a detailed view of a state. Includes all relevant 
/// details about a state, such as name, description, capital, area, and active status.
/// </summary>
public record ItemStateDto(
    Guid Id,
    string Name,
    string Description,
    string Capital,
    long Population,
    double AreaKm2,
    bool IsActive,
    string Country,
    Guid Version
);
