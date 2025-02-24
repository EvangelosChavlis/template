namespace server.src.Domain.Geography.Natural.Timezones.Dtos;

/// <summary>
/// DTO representing a timezone item in a list.
/// </summary>
/// <param name="Id">The unique identifier of the timezone.</param>
/// <param name="Name">The name of the timezone.</param>
/// <param name="UtcOffset">The standard UTC offset of the timezone.</param>
/// <param name="IsActive">Indicates whether the timezone is active.</param>
/// <param name="Count">The number of associated items.</param>
public record ListItemTimezoneDto(
    Guid Id,
    string Name,
    double UtcOffset,
    bool IsActive,
    int Count
);