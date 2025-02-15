// source
using server.src.Domain.Weather.Warnings.Dtos;
using server.src.Domain.Weather.Warnings.Models;

namespace server.src.Application.Weather.Warnings.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Warning"/> model 
/// into a <see cref="ListItemWarningDto"/>.
/// This utility class is used to transform warning data for list views with key details.
/// </summary>
public static class ListItemWarningDtoMapper
{
    /// <summary>
    /// Maps a <see cref="Warning"/> model to a <see cref="ListItemWarningDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="Warning"/> model that will be mapped.</param>
    /// <returns>A <see cref="ListItemWarningDto"/> representing the warning model with essential details.</returns>
    public static ListItemWarningDto ListItemWarningDtoMapping(
        this Warning model) => new(
            Id: model.Id,
            Name: model.Name,
            Description: model.Description,
            Count: model.Forecasts.Count
        );
}