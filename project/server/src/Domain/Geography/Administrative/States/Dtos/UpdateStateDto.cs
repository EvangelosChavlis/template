namespace server.src.Domain.Geography.Administrative.States.Dtos;

/// <summary>
/// DTO for updating an existing state. Used to encapsulate the data for updating a state's details.
/// </summary>
public record UpdateStateDto(
    string Name,
    string Description,
    string Capital,
    long Population,
    double AreaKm2,
    bool IsActive,
    Guid CountryId,
    Guid Version
);