namespace server.src.Domain.Weather.Collections.Forecasts.Dtos;

/// <summary>
/// Represents a simplified forecast item containing basic information
/// about the forecast date, temperature, and associated warning.
/// </summary>
public record ListItemForecastDto(
    Guid Id, 
    string Date, 
    int TemperatureC,
    int Humidity,
    string Warning,
    string MoonPhase,
    string Station
);