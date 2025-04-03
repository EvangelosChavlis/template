// source
using server.src.Domain.Geography.Natural.Timezones.Dtos;
using server.src.Domain.Geography.Natural.Timezones.Models;

namespace server.src.Application.Geography.Natural.Timezones.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Timezone"/> model 
/// into a <see cref="ListItemTimezoneDto"/>.
/// This utility class is used to transform timezone data for list views with key details.
/// </summary>
public static class ListItemTimezoneDtoMapper
{
    /// <summary>
    /// Maps a <see cref="Timezone"/> model to a <see cref="ListItemTimezoneDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="Timezone"/> model that will be mapped.</param>
    /// <returns>A <see cref="ListItemTimezoneDto"/> representing the timezone model with essential details.</returns>
    public static ListItemTimezoneDto ListItemTimezoneDtoMapping(
        this Timezone model) => new(
            Id: model.Id,
            Name: model.Name,
            Code: model.Code,
            UtcOffset: model.UtcOffset,
            IsActive: model.IsActive,
            Count: model.Locations.Count
        );
}