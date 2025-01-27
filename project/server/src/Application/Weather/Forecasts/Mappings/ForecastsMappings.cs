// source
using server.src.Domain.Dto.Weather;
using server.src.Domain.Extensions;
using server.src.Domain.Models.Weather;

namespace server.src.Application.Weather.Forecasts.Mappings;

/// <summary>
/// Contains static mapping methods to transform Forecast models into their corresponding DTOs.
/// </summary>
public static class ForecastsMappings
{
    /// <summary>
    /// Maps a <see cref="Forecast"/> model to a <see cref="StatItemForecastDto"/>.
    /// This method extracts the forecast's ID, formatted date, and temperature to create a simplified statistical forecast item.
    /// </summary>
    /// <param name="model">The <see cref="Forecast"/> object containing the forecast data.</param>
    /// <returns>A <see cref="StatItemForecastDto"/> containing the forecast's ID, formatted date, and temperature.</returns>
    public static StatItemForecastDto StatItemForecastDtoMapping(
        this Forecast model) => new(
            Id: model.Id,
            Date: model.Date.GetFullLocalDateTimeString(),
            TemperatureC: model.TemperatureC
        );


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
            IsRead: model.IsRead,
            Warning: model.Warning.Name
        );

    /// <summary>
    /// Maps a Forecast model to an ItemForecastDto.
    /// </summary>
    /// <param name="model">The Forecast model that will be mapped to an ItemForecastDto.</param>
    /// <returns>An ItemForecastDto representing the Forecast model with full details for an individual item view.</returns>
    public static ItemForecastDto ItemForecastDtoMapping(
        this Forecast model) => new(
            Id: model.Id,
            Date: model.Date.GetFullLocalDateTimeString(),
            TemperatureC: model.TemperatureC,
            Summary: model.Summary,
            Warning: model.Warning.Name,
            Version: model.Version
        );

    /// <summary>
    /// Maps a Forecast model to an ItemForecastDto with default values in case of an error.
    /// </summary>
    /// <param name="model">The Forecast model that will be mapped to an ItemForecastDto.</param>
    /// <returns>An ItemForecastDto with default values representing an error state.</returns>
    public static ItemForecastDto ErrorItemForecastDtoMapping() 
        => new (
            Id: Guid.Empty,
            Date: string.Empty,
            TemperatureC: int.MinValue,
            Summary: string.Empty,
            Warning: string.Empty,
            Version: Guid.Empty
        );

    /// <summary>
    /// Maps a ForecastDto to a Forecast model, creating a new Forecast.
    /// </summary>
    /// <param name="dto">The ForecastDto that contains data to create the Forecast model.</param>
    /// <param name="warningModel">The Warning model associated with the forecast.</param>
    /// <returns>A Forecast model populated with data from the ForecastDto and associated Warning model.</returns>
    public static Forecast CreateForecastModelMapping(this ForecastDto dto, Warning warningModel)
        => new()
        {
            Date = dto.Date,
            TemperatureC = dto.TemperatureC,
            Summary = dto.Summary,
            IsRead = false,
            Longitude = dto.Latitude,
            Latitude = dto.Latitude,
            Version = Guid.NewGuid(),
            WarningId = warningModel.Id,
            Warning = warningModel
        };

    /// <summary>
    /// Updates an existing Forecast model with data from a ForecastDto and associated Warning model.
    /// </summary>
    /// <param name="dto">The ForecastDto containing updated data for the Forecast model.</param>
    /// <param name="model">The Forecast model to be updated.</param>
    /// <param name="warningModel">The Warning model to be associated with the updated Forecast.</param>
    public static void UpdateForecastModelMapping(this ForecastDto dto, 
        Forecast model, Warning warningModel)
    {
        model.Date = dto.Date;
        model.TemperatureC = dto.TemperatureC;
        model.Summary = dto.Summary;
        model.Longitude = dto.Latitude;
        model.Latitude = dto.Latitude;
        model.WarningId = warningModel.Id;
        model.Warning = warningModel;
        model.Version = Guid.NewGuid(); 
    }
}