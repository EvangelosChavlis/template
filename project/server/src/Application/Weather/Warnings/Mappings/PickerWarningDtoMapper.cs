// source
using server.src.Domain.Weather.Warnings.Dtos;
using server.src.Domain.Weather.Warnings.Models;

namespace server.src.Application.Weather.Warnings.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Warning"/> model 
/// into a <see cref="PickerWarningDto"/>.
/// This mapper is used to transform warning data for selection lists or dropdowns.
/// </summary>
public static class PickerWarningDtoMapper
{
    /// <summary>
    /// Maps a <see cref="Warning"/> model to a <see cref="PickerWarningDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="Warning"/> model that will be mapped.</param>
    /// <returns>A <see cref="PickerWarningDto"/> containing essential details for selection purposes.</returns>
    public static PickerWarningDto PickerWarningDtoMapping(
        this Warning model) => new(
            Id: model.Id,
            Name: model.Name
        );
}
