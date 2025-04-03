// source
using server.src.Domain.Common.Extensions;
using server.src.Domain.Weather.Collections.Observations.Dtos;
using server.src.Domain.Weather.Collections.Observations.Models;

namespace server.src.Application.Weather.Collections.Observations.Mappings;

public static class ListItemObservationDtoMapper
{
    /// <summary>
    /// Maps a Observation model to a ListItemObservationDto.
    /// </summary>
    /// <param name="model">The Observation model that will be mapped to a ListItemObservationDto.</param>
    /// <returns>A ListItemObservationDto representing the Observation model with key details for a list view.</returns>
    public static ListItemObservationDto ListItemObservationDtoMapping(
        this Observation model) => new(
            Id: model.Id,
            Timestamp: model.Timestamp.GetFullLocalDateTimeString(),
            TemperatureC: model.TemperatureC,
            Humidity: model.Humidity,
            MoonPhase: model.MoonPhase.Name,
            Station: model.Station.Name
        );
}