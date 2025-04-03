// source
using server.src.Domain.Geography.Administrative.Stations.Dtos;
using server.src.Domain.Geography.Administrative.Stations.Models;

namespace server.src.Application.Geography.Natural.Stations.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Station"/> model 
/// into a <see cref="ListItemStationDto"/>.
/// This utility class is used to transform country data for list views with key details.
/// </summary>
public static class ListItemStationDtoMapper
{
    /// <summary>
    /// Maps a <see cref="Station"/> model to a <see cref="ListItemStationDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="Station"/> model that will be mapped.</param>
    /// <returns>A <see cref="ListItemStationDto"/> representing the country model with essential details.</returns>
    public static ListItemStationDto ListItemStationDtoMapping(
        this Station model) => new(
            Id: model.Id,
            Name: model.Name,
            Code: model.Code,
            IsActive: model.IsActive,
            Observations: model.Observations.Count,
            Forecasts: model.Forecasts.Count
        );
}
