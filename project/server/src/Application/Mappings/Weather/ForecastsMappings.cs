// source
using server.src.Domain.Dto.Weather;
using server.src.Domain.Extensions;
using server.src.Domain.Models.Weather;

namespace server.src.Application.Mappings.Weather;

public static class ForecastsMappings
{
    public static ListItemForecastDto ListItemForecastDtoMapping(
        this Forecast model) => new(
            Id: model.Id,
            Date: model.Date.GetFullLocalDateTimeString(),
            TemperatureC: model.TemperatureC,
            Warning: model.Warning.Name
        );


   public static ItemForecastDto ItemForecastDtoMapping(
        this Forecast model) => new(
            Id: model.Id,
            Date: model.Date.GetFullLocalDateTimeString(),
            TemperatureC: model.TemperatureC,
            Summary: model.Summary,
            Warning: model.Warning.Name
        );


    public static Forecast CreateForecastModelMapping(this ForecastDto dto, Warning warningModel)
        => new()
        {
            Date = dto.Date,
            TemperatureC = dto.TemperatureC,
            Summary = dto.Summary,
            WarningId = warningModel.Id,
            Warning = warningModel
        };


    public static void UpdateForecastMapping(this ForecastDto dto, Forecast model, Warning warningModel)
    {
        model.Date = dto.Date;
        model.TemperatureC = dto.TemperatureC;
        model.Summary = dto.Summary;
        model.WarningId = warningModel.Id;
        model.Warning = warningModel;
    }
}