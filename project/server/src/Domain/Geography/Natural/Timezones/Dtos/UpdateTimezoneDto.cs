namespace server.src.Domain.Geography.Natural.Timezones.Dtos;

/// <summary>
/// DTO for updating an existing timezone.
/// </summary>
/// <param name="Name">The updated name of the timezone.</param>
/// <param name="UtcOffset">The updated standard UTC offset of the timezone.</param>
/// <param name="SupportsDaylightSaving">Indicates whether the timezone supports daylight saving time.</param>
/// <param name="Description">The updated description of the timezone.</param>
/// <param name="DstOffset">The updated daylight saving time (DST) offset, if applicable.</param>
/// <param name="Version">The version identifier for concurrency control.</param>
public record UpdateTimezoneDto(
    string Name,
    double UtcOffset,
    bool SupportsDaylightSaving,
    string Description,
    double? DstOffset,
    Guid Version
);