namespace server.src.Domain.Geography.Timezones.Dtos;

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