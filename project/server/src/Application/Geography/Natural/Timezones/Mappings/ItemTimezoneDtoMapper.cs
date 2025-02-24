// source
using server.src.Domain.Geography.Natural.Timezones.Dtos;
using server.src.Domain.Geography.Natural.Timezones.Models;

namespace server.src.Application.Geography.Natural.Timezones.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Timezone"/> model  
/// into an <see cref="ItemTimezoneDto"/> for detailed item representation.
/// </summary>
public static class ItemTimezoneDtoMapper
{
    /// <summary>
    /// Maps a Timezone model to an ItemTimezoneDto.
    /// </summary>
    /// <param name="model">The Timezone model that will be mapped to an ItemTimezoneDto.</param>
    /// <returns>An ItemTimezoneDto representing the Timezone model with full details for an individual item view.</returns>
    public static ItemTimezoneDto ItemTimezoneDtoMapping(
        this Timezone model) => new(
            Id: model.Id,
            Name: model.Name,
            UtcOffset: model.UtcOffset,
            SupportsDaylightSaving: model.SupportsDaylightSaving,
            IsActive: model.IsActive,
            Description: model.Description,
            DstOffset: model.DstOffset,
            Version: model.Version
        );
}