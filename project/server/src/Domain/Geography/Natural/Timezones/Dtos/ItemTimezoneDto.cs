namespace server.src.Domain.Geography.Natural.Timezones.Dtos;

/// <summary>
/// DTO representing a timezone item, including additional details like the standardized code.
/// </summary>
/// <param name="Id">The unique identifier of the timezone.</param>
/// <param name="Name">The name of the timezone (e.g., "Pacific Standard Time").</param>
/// <param name="Description">A brief description of the timezone, providing additional context.</param>
/// <param name="Code">The standardized code representing the timezone (e.g., "PST", "UTC+5").</param>
/// <param name="UtcOffset">The standard UTC offset of the timezone (e.g., -8 for PST, 0 for UTC).</param>
/// <param name="SupportsDaylightSaving">Indicates whether the timezone supports daylight saving time (DST).</param>
/// <param name="IsActive">Indicates whether the timezone is currently active and available for use.</param>
/// <param name="DstOffset">The daylight saving time (DST) offset, if applicable. If no DST, this value is null.</param>
/// <param name="Version">The version identifier for concurrency control in data updates.</param>
public record ItemTimezoneDto(
    Guid Id,
    string Name,
    string Description,
    string Code,
    double UtcOffset,
    bool SupportsDaylightSaving,
    double? DstOffset,
    bool IsActive,
    Guid Version
);
