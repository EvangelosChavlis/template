namespace server.src.Domain.Geography.Natural.Timezones.Dtos;

/// <summary>
/// DTO representing a timezone item.
/// </summary>
/// <param name="Id">The unique identifier of the timezone.</param>
/// <param name="Name">The name of the timezone.</param>
/// <param name="UtcOffset">The standard UTC offset of the timezone.</param>
/// <param name="SupportsDaylightSaving">Indicates whether the timezone supports daylight saving time.</param>
/// <param name="IsActive">Indicates whether the timezone is active.</param>
/// <param name="Description">A brief description of the timezone.</param>
/// <param name="DstOffset">The daylight saving time (DST) offset, if applicable.</param>
/// <param name="Version">The version identifier for concurrency control.</param>
public record ItemTimezoneDto(
    Guid Id,
    string Name,
    double UtcOffset,
    bool SupportsDaylightSaving,
    bool IsActive,
    string Description,
    double? DstOffset,
    Guid Version
);