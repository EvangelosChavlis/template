// source
using server.src.Domain.Dto.Weather;
using server.src.Domain.Extensions;
using server.src.Domain.Models.Geography;
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
            Humidity: model.Humidity,
            Warning: model.Warning.Name,
            MoonPhase: model.MoonPhase.Name,
            Longitude: model.Location.Longitude,
            Latitude: model.Location.Latitude,
            Altitude: model.Location.Altitude,
            IsRead: model.IsRead
        );

    /// <summary>
    /// Maps a Forecast model to an ItemForecastDto.
    /// </summary>
    /// <param name="model">The Forecast model that will be mapped to an ItemForecastDto.</param>
    /// <returns>An ItemForecastDto representing the Forecast model with full details for an individual item view.</returns>
    public static ItemForecastDto ItemForecastDtoMapping(this Forecast model) => new(
        Id: model.Id,
        Date: model.Date.GetFullLocalDateTimeString(),
        TemperatureC: model.TemperatureC,
        TemperatureF: model.TemperatureF,
        FeelsLikeC: model.FeelsLikeC,
        Humidity: model.Humidity,
        WindSpeedKph: model.WindSpeedKph,
        WindDirection: model.WindDirection,
        PressureHpa: model.PressureHpa,
        PrecipitationMm: model.PrecipitationMm,
        VisibilityKm: model.VisibilityKm,
        UVIndex: model.UVIndex,
        AirQualityIndex: model.AirQualityIndex,
        CloudCover: model.CloudCover,
        LightningProbability: model.LightningProbability,
        PollenCount: model.PollenCount,
        Sunrise: model.Sunrise,
        Sunset: model.Sunset,
        Longitude: model.Location.Longitude,
        Latitude: model.Location.Latitude,
        Altitude: model.Location.Altitude,
        Summary: model.Summary,
        Warning: model.Warning.Name,
        Version: model.Version
    );


    /// <summary>
    /// Maps a Forecast model to an ItemForecastDto with default values in case of an error.
    /// </summary>
    /// <returns>An ItemForecastDto with default values representing an error state.</returns>
    public static ItemForecastDto ErrorItemForecastDtoMapping() 
        => new (
            Id: Guid.Empty,
            Date: string.Empty,
            TemperatureC: int.MinValue,
            TemperatureF: int.MinValue,
            FeelsLikeC: int.MinValue,
            Humidity: int.MinValue,
            WindSpeedKph: double.MinValue,
            WindDirection: int.MinValue,
            PressureHpa: double.MinValue,
            PrecipitationMm: double.MinValue,
            VisibilityKm: double.MinValue,
            UVIndex: int.MinValue,
            AirQualityIndex: int.MinValue,
            CloudCover: int.MinValue,
            LightningProbability: int.MinValue,
            PollenCount: int.MinValue,
            Sunrise: TimeSpan.MinValue,
            Sunset: TimeSpan.MinValue,
            Longitude: double.MinValue,
            Latitude: double.MinValue,
            Altitude: double.MinValue,
            Summary: string.Empty,
            Warning: string.Empty,
            Version: Guid.Empty
        );

    
    /// <summary>
    /// Creates a new <see cref="Forecast"/> model from a <see cref="CreateForecastDto"/>.
    /// </summary>
    /// <param name="dto">The data transfer object containing forecast details.</param>
    /// <param name="warningModel">The warning model associated with the forecast.</param>
    /// <param name="locationModel">The location model where the forecast applies.</param>
    /// <param name="moonPhase">The moon phase model linked to the forecast.</param>
    /// <returns>A new instance of <see cref="Forecast"/> populated with data from the provided DTO and related models.</returns>
    public static Forecast CreateForecastModelMapping(
        this CreateForecastDto dto, 
        Warning warningModel, 
        Location locationModel, 
        MoonPhase moonPhase)
        => new()
        {
            Date = dto.Date,
            TemperatureC = dto.TemperatureC,
            FeelsLikeC = dto.FeelsLikeC,
            Humidity = dto.Humidity,
            WindSpeedKph = dto.WindSpeedKph,
            WindDirection = dto.WindDirection,
            PressureHpa = dto.PressureHpa,
            PrecipitationMm = dto.PrecipitationMm,
            VisibilityKm = dto.VisibilityKm,
            UVIndex = dto.UVIndex,
            AirQualityIndex = dto.AirQualityIndex,
            CloudCover = dto.CloudCover,
            LightningProbability = dto.LightningProbability,
            PollenCount = dto.PollenCount,
            Sunrise = dto.Sunrise,
            Sunset = dto.Sunset,
            Summary = dto.Summary,
            IsRead = false,
            WarningId = warningModel.Id,
            Warning = warningModel,
            LocationId = locationModel.Id,
            Location = locationModel,
            MoonPhaseId = moonPhase.Id,
            MoonPhase = moonPhase,
            Version = Guid.NewGuid()
        };

    /// <summary>
    /// Updates an existing <see cref="Forecast"/> model with new data from an <see cref="UpdateForecastDto"/>.
    /// </summary>
    /// <param name="dto">The data transfer object containing updated forecast details.</param>
    /// <param name="model">The existing forecast model to be updated.</param>
    /// <param name="warningModel">The updated warning model linked to the forecast.</param>
    /// <param name="locationModel">The updated location model where the forecast applies.</param>
    /// <param name="moonPhaseModel">The updated moon phase model associated with the forecast.</param>
    public static void UpdateForecastModelMapping(
        this UpdateForecastDto dto, 
        Forecast model, 
        Warning warningModel, 
        Location locationModel, 
        MoonPhase moonPhaseModel)
    {
        model.Date = dto.Date;
        model.TemperatureC = dto.TemperatureC;
        model.FeelsLikeC = dto.FeelsLikeC;
        model.Humidity = dto.Humidity;
        model.WindSpeedKph = dto.WindSpeedKph;
        model.WindDirection = dto.WindDirection;
        model.PressureHpa = dto.PressureHpa;
        model.PrecipitationMm = dto.PrecipitationMm;
        model.VisibilityKm = dto.VisibilityKm;
        model.UVIndex = dto.UVIndex;
        model.AirQualityIndex = dto.AirQualityIndex;
        model.CloudCover = dto.CloudCover;
        model.LightningProbability = dto.LightningProbability;
        model.PollenCount = dto.PollenCount;
        model.Sunrise = dto.Sunrise;
        model.Sunset = dto.Sunset;
        model.Summary = dto.Summary;
        model.LocationId = locationModel.Id;
        model.Location = locationModel;
        model.WarningId = warningModel.Id;
        model.Warning = warningModel;
        model.MoonPhase = moonPhaseModel;
        model.MoonPhaseId = moonPhaseModel.Id;
        model.Version = Guid.NewGuid();
    }
}