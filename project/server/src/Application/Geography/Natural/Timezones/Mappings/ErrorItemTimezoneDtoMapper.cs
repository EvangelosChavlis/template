// source
using server.src.Domain.Geography.Natural.Timezones.Dtos;

namespace server.src.Application.Geography.Natural.Timezones.Mappings;

/// <summary>
/// Provides a method to generate an <see cref="ItemTimezoneDto"/> with default values  
/// to represent an error state when a valid timezone cannot be retrieved.
/// </summary>
public class ErrorItemTimezoneDtoMapper
{
    /// <summary>
    /// Creates an <see cref="ItemTimezoneDto"/> with default values in case of an error.
    /// </summary>
    /// <returns>An <see cref="ItemTimezoneDto"/> with empty or placeholder values, indicating an error state.</returns>
    public static ItemTimezoneDto ErrorItemTimezoneDtoMapping() => 
        new (
            Id: Guid.Empty,
            Name: string.Empty,
            Description: string.Empty,
            Code: string.Empty,
            UtcOffset: double.MinValue,
            DstOffset: double.MinValue,
            SupportsDaylightSaving: false,
            IsActive: false,
            Version: Guid.Empty 
        );
}