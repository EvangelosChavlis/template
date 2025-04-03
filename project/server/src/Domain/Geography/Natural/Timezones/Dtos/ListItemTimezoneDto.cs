namespace server.src.Domain.Geography.Natural.Timezones.Dtos;

/// <summary>
/// DTO representing a timezone item in a list, including the standardized timezone code and the active status.
/// </summary>
/// <param name="Id">The unique identifier of the timezone.</param>
/// <param name="Name">The name of the timezone (e.g., "Pacific Standard Time").</param>
/// <param name="Code">The standardized code representing the timezone (e.g., "PST", "UTC+5").</param>
/// <param name="UtcOffset">The standard UTC offset of the timezone (e.g., -8 for PST, 0 for UTC).</param>
/// <param name="IsActive">Indicates whether the timezone is currently active and available for use.</param>
/// <param name="Count">The number of locations or associated items within the timezone.</param>
public record ListItemTimezoneDto(
    Guid Id,
    string Name,
    string Code,
    double UtcOffset,
    bool IsActive,
    int Count
);