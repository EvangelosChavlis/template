namespace server.src.Domain.Geography.Natural.Timezones.Dtos;

/// <summary>
/// DTO for creating a new timezone.
/// </summary>
/// <param name="Name">The name of the timezone.</param>
/// <param name="UtcOffset">The standard UTC offset of the timezone.</param>
/// <param name="SupportsDaylightSaving">Indicates whether the timezone supports daylight saving time.</param>
/// <param name="Description">A brief description of the timezone.</param>
/// <param name="DstOffset">The daylight saving time (DST) offset, if applicable.</param>
public record CreateTimezoneDto(
    string Name,
    double UtcOffset,
    bool SupportsDaylightSaving,
    string Description,
    double? DstOffset
);