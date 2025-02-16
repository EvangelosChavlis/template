namespace server.src.Domain.Geography.Timezones.Dtos;

public record UpdateTimezoneDto(
    string Name,
    double UtcOffset,
    bool SupportsDaylightSaving,
    string Description,
    double? DstOffset,
    Guid Version
);