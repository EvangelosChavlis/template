// source
using server.src.Domain.Dto.Weather;
using server.src.Domain.Extensions;
using server.src.Domain.Models.Weather;

namespace server.src.Application.Mappings.Weather;

public static class WarningsMappings
{
    public static ListItemWarningDto ListItemWarningDtoMapping(
        this Warning model) => new(
            Id: model.Id,
            Name: model.Name,
            Description: model.Description,
            Count: model.Forecasts.Count()
        );


    public static PickerWarningDto PickerWarningDtoMapping(
        this Warning model) => new(
            Id: model.Id,
            Name: model.Name
        );


   public static ItemWarningDto ItemWarningDtoMapping(
        this Warning model) => new(
            Id: model.Id,

            Name: model.Name,
            Description: model.Description,

            Forecasts: model.Forecasts.Select(c => $"{c.Date.GetFullLocalDateTimeString()}, {c.TemperatureC.ToString()}" ).ToList()
        );

    public static Warning CreateWarningModelMapping(this WarningDto dto)
        => new()
        {
            Name = dto.Name,
            Description = dto.Description
        };

    public static void UpdateWarningMapping(this WarningDto dto, Warning model)
    {
        model.Name = dto.Name;
        model.Description = dto.Description;
    }
}