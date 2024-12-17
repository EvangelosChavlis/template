namespace server.src.Domain.Dto.Weather;

/// <summary>
/// Represents a simplified forecast item containing basic information
/// about the forecast date, temperature, and associated warning.
/// </summary>
public record ListItemForecastDto(
    Guid Id, 
    string Date, 
    int TemperatureC,
    string Warning
);

/// <summary>
/// Represents a detailed forecast item containing the full date, 
/// temperature, summary, and warning details.
/// </summary>
public record ItemForecastDto(
    Guid Id,
    string Date,
    int TemperatureC,
    string Summary,
    string Warning
);

/// <summary>
/// Represents a forecast data transfer object containing detailed 
/// information about the forecast's date, temperature, summary, and related warning.
/// </summary>
public record ForecastDto(
    DateTime Date, 
    int TemperatureC,
    string Summary,
    Guid WarningId
);
