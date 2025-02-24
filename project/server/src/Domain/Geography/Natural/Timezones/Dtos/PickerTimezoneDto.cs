namespace server.src.Domain.Geography.Natural.Timezones.Dtos;

/// <summary>
/// DTO for selecting a timezone.
/// </summary>
/// <param name="Id">The unique identifier of the timezone.</param>
/// <param name="Name">The name of the timezone.</param>
public record PickerTimezoneDto(
    Guid Id,
    string Name
);