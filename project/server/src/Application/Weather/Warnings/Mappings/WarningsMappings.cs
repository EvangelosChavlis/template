// source
using server.src.Domain.Dto.Weather;
using server.src.Domain.Models.Weather;

namespace server.src.Application.Weather.Warnings.Mappings;

/// <summary>
/// Contains static mapping methods to transform Warning models into their corresponding DTOs.
/// </summary>
public static class WarningsMappings
{
    /// <summary>
    /// Maps a Warning model to a ListItemWarningDto.
    /// </summary>
    /// <param name="model">The Warning model that will be mapped to a ListItemWarningDto.</param>
    /// <returns>A ListItemWarningDto representing the Warning model with key details for a list view.</returns>
    public static ListItemWarningDto ListItemWarningDtoMapping(
        this Warning model) => new(
            Id: model.Id,
            Name: model.Name,
            Description: model.Description,
            Count: model.Forecasts.Count
        );

    /// <summary>
    /// Maps a Warning model to a PickerWarningDto.
    /// </summary>
    /// <param name="model">The Warning model that will be mapped to a PickerWarningDto.</param>
    /// <returns>A PickerWarningDto representing the Warning model with essential details for selection purposes.</returns>
    public static PickerWarningDto PickerWarningDtoMapping(
        this Warning model) => new(
            Id: model.Id,
            Name: model.Name
        );

    /// <summary>
    /// Maps a Warning model to an ItemWarningDto.
    /// </summary>
    /// <param name="model">The Warning model that will be mapped to an ItemWarningDto.</param>
    /// <returns>An ItemWarningDto representing the Warning model with full details for an individual item view.</returns>
    public static ItemWarningDto ItemWarningDtoMapping(
        this Warning model) => new(
            Id: model.Id,
            Name: model.Name,
            Description: model.Description,
            Version: model.Version
        );


    /// <summary>
    /// Maps a Warning model to an ItemWarningDto with default values in case of an error.
    /// </summary>
    /// <param name="model">The Warning model that will be mapped to an ItemWarningDto.</param>
    /// <returns>An ItemWarningDto with default values representing an error state.</returns>
    public static ItemWarningDto ErrorItemWarningDtoMapping() => 
        new (
            Id: Guid.Empty,
            Name: string.Empty,
            Description: string.Empty,
            Version: Guid.Empty 
        );

    /// <summary>
    /// Maps a WarningDto to a Warning model, creating a new Warning.
    /// </summary>
    /// <param name="dto">The WarningDto that contains data to create the Warning model.</param>
    /// <returns>A Warning model populated with data from the WarningDto.</returns>
    public static Warning CreateWarningModelMapping(this WarningDto dto)
        => new()
        {
            Name = dto.Name,
            Description = dto.Description,
            IsActive = true,
            Version = Guid.NewGuid()
        };

    /// <summary>
    /// Updates an existing Warning model with data from a WarningDto.
    /// </summary>
    /// <param name="dto">The WarningDto containing updated data for the Warning model.</param>
    /// <param name="model">The Warning model to be updated.</param>
    public static void UpdateWarningMapping(this WarningDto dto, Warning model)
    {
        model.Name = dto.Name;
        model.Description = dto.Description;
        model.Version = Guid.NewGuid();
    }
}
