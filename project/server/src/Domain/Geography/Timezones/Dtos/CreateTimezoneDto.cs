namespace server.src.Domain.Geography.Timezones.Dtos;

public record CreateTimezoneDto(
    string Name,
    double UtcOffset,
    bool SupportsDaylightSaving,
    string Description,
    double? DstOffset
);