namespace server.src.Domain.Geography.Administrative.States.Dtos;

/// <summary>
/// DTO for listing states. Used for displaying a 
// summarized view of a state in lists, including name, status, and population count.
/// </summary>
public record ListItemStateDto(
    Guid Id,
    string Name,
    bool IsActive,
    long Population,
    int Count
);