namespace server.src.Domain.Dto.Weather;

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
    double Altitude,
    bool IsRead
);

/// <summary>
/// Represents a detailed forecast item containing the full date, 
/// temperature, summary, and warning details.
/// </summary>
public record ItemForecastDto(
    Guid Id,
    string Date,
    int TemperatureC,
    int TemperatureF,
    int FeelsLikeC,
    int Humidity,
    double WindSpeedKph,
    int WindDirection,
    double PressureHpa,
    double PrecipitationMm,
    double VisibilityKm,
    int UVIndex,
    int AirQualityIndex,
    int CloudCover,
    int LightningProbability,
    int PollenCount,
    TimeSpan Sunrise,
    TimeSpan Sunset,
    double Longitude,
    double Latitude,
    double Altitude,
    string Summary,
    string Warning,
    Guid Version
);

/// <summary>
/// Represents a forecast data transfer object containing detailed 
/// meteorological information, including temperature, humidity, wind, 
/// atmospheric pressure, and related warnings.
/// </summary>
public record CreateForecastDto(
    DateTime Date, 
    int TemperatureC,
    int FeelsLikeC,
    int Humidity,
    double WindSpeedKph,
    int WindDirection,
    double PressureHpa,
    double PrecipitationMm,
    double VisibilityKm,
    int UVIndex,
    int AirQualityIndex,
    int CloudCover,
    int LightningProbability,
    int PollenCount,
    TimeSpan Sunrise,
    TimeSpan Sunset,
    string Summary,
    Guid LocationId,
    Guid WarningId,
    Guid MoonPhaseId
);


/// <summary>
/// Represents a forecast data transfer object containing detailed 
/// meteorological information, including temperature, humidity, wind, 
/// atmospheric pressure, and related warnings.
/// </summary>
public record UpdateForecastDto(
    DateTime Date, 
    int TemperatureC,
    int FeelsLikeC,
    int Humidity,
    double WindSpeedKph,
    int WindDirection,
    double PressureHpa,
    double PrecipitationMm,
    double VisibilityKm,
    int UVIndex,
    int AirQualityIndex,
    int CloudCover,
    int LightningProbability,
    int PollenCount,
    TimeSpan Sunrise,
    TimeSpan Sunset,
    string Summary,
    Guid LocationId,
    Guid WarningId,
    Guid MoonPhaseId,
    Guid Version
);
