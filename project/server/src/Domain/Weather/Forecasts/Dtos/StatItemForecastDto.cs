namespace server.src.Domain.Weather.Forecasts.Dtos;

/// <summary>
/// Represents a simplified forecast containing basic information 
/// about the forecast's date and temperature.
/// This record is primarily used to store statistical data related 
/// to weather forecasts.
/// </summary>
public record StatItemForecastDto(
    Guid Id, 
    string Date, 
    int TemperatureC
);
