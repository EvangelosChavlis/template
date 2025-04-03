// source
using server.src.Domain.Common.Extensions;
using server.src.Domain.Weather.Collections.Forecasts.Dtos;
using server.src.Domain.Weather.Collections.Forecasts.Models;

namespace server.src.Application.Weather.Collections.Forecasts.Mappings;

public static class ListItemForecastDtoMapper
{
    /// <summary>
    /// Maps a Forecast model to a ListItemForecastDto.
    /// </summary>
    /// <param name="model">The Forecast model that will be mapped to a ListItemForecastDto.</param>
    /// <returns>A ListItemForecastDto representing the Forecast model with key details for a list view.</returns>
    public static ListItemForecastDto ListItemForecastDtoMapping(
        this Forecast model) => new(
            Id: model.Id,
            Date: model.Date.GetFullLocalDateTimeString(),
            TemperatureC: model.TemperatureC,
            Humidity: model.Humidity,
            Warning: model.Warning.Name,
            MoonPhase: model.MoonPhase.Name,
            Station: model.Station.Name
        );
}