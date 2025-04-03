// source
using server.src.Domain.Common.Extensions;
using server.src.Domain.Weather.Collections.Forecasts.Dtos;
using server.src.Domain.Weather.Collections.Forecasts.Models;

namespace server.src.Application.Weather.Collections.Forecasts.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Forecast"/> model 
/// into a simplified <see cref="StatItemForecastDto"/> containing key statistical data.
/// </summary>
public static class StatItemForecastDtoMapper
{
    /// <summary>
    /// Maps a <see cref="Forecast"/> model to a <see cref="StatItemForecastDto"/>, 
    /// extracting the forecast's ID, formatted date, and temperature for statistical purposes.
    /// </summary>
    /// <param name="model">The forecast model to be mapped.</param>
    /// <returns>A <see cref="StatItemForecastDto"/> containing the forecast's ID, formatted date, and temperature.</returns>
    public static StatItemForecastDto StatItemForecastDtoMapping(
        this Forecast model) => new(
            Id: model.Id,
            Date: model.Date.GetFullLocalDateTimeString(),
            TemperatureC: model.TemperatureC
        );
}