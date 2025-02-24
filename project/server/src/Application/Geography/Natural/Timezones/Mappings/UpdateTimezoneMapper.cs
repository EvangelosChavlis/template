// source
using server.src.Domain.Geography.Natural.Timezones.Dtos;
using server.src.Domain.Geography.Natural.Timezones.Models;

namespace server.src.Application.Geography.Natural.Timezones.Mappings;

/// <summary>
/// Provides mapping functionality to update an existing <see cref="Timezone"/> model 
/// using data from an <see cref="UpdateTimezoneDto"/>.
/// This utility class ensures that the timezone entity is updated efficiently with new details.
/// </summary>
public static class UpdateTimezoneMapper
{
    /// <summary>
    /// Updates an existing <see cref="Timezone"/> model with data from an <see cref="UpdateTimezoneDto"/>.
    /// </summary>
    /// <param name="dto">The data transfer object containing updated timezone details.</param>
    /// <param name="model">The existing <see cref="Timezone"/> model to be updated.</param>
    public static void UpdateTimezoneMapping(this UpdateTimezoneDto dto, Timezone model)
    {
        model.Name = dto.Name;
        model.UtcOffset = dto.UtcOffset;
        model.SupportsDaylightSaving = dto.SupportsDaylightSaving;
        model.IsActive = model.IsActive;
        model.Description = dto.Description;
        model.DstOffset = dto.DstOffset;
        model.Version = Guid.NewGuid();
    }
}
