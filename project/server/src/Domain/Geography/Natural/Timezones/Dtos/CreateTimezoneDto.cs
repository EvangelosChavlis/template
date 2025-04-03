/// <summary>
/// Data Transfer Object (DTO) for creating a new timezone.
/// This record is used to pass timezone data from the client to the server during creation.
/// </summary>
/// <param name="Name">The name of the timezone (e.g., "Pacific Standard Time").</param>
/// <param name="Description">A brief description of the timezone, providing additional context.</param>
/// <param name="Code">The standardized code representing the timezone (e.g., "PST", "UTC+5").</param>
/// <param name="UtcOffset">The standard UTC offset of the timezone (e.g., -8 for PST, 0 for UTC).</param>
/// <param name="SupportsDaylightSaving">Indicates whether the timezone follows daylight saving time (DST).</param>
/// <param name="DstOffset">
/// The daylight saving time (DST) offset, if applicable.
/// If the timezone does not support DST, this value is null.
/// </param>
public record CreateTimezoneDto(
    string Name,
    string Description,
    string Code,
    double UtcOffset,
    bool SupportsDaylightSaving,
    double? DstOffset
);