namespace server.src.Domain.Weather.Forecasts.Dtos;

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
    double Longitude,
    double Latitude,
    double Altitude
);