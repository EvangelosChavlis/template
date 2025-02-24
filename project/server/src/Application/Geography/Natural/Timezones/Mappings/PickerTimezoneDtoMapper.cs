// source
using server.src.Domain.Geography.Natural.Timezones.Dtos;
using server.src.Domain.Geography.Natural.Timezones.Models;

namespace server.src.Application.Geography.Natural.Timezones.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Timezone"/> model 
/// into a <see cref="PickerTimezoneDto"/>.
/// This mapper is used to transform timezone data for selection lists or dropdowns.
/// </summary>
public static class PickerTimezoneDtoMapper
{
    /// <summary>
    /// Maps a <see cref="Timezone"/> model to a <see cref="PickerTimezoneDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="Timezone"/> model that will be mapped.</param>
    /// <returns>A <see cref="PickerTimezoneDto"/> containing essential details for selection purposes.</returns>
    public static PickerTimezoneDto PickerTimezoneDtoMapping(
        this Timezone model) => new(
            Id: model.Id,
            Name: model.Name
        );
}
