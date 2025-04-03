// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Collections.Forecasts.Extensions;
using server.src.Domain.Weather.Collections.Forecasts.Models;

namespace server.src.Application.Weather.Collections.Forecasts.Validators;

public static class ForecastModelValidators
{
    public static ValidationResult Validate(this Forecast model)
    {
        var errors = new List<string>();

        // Validation for Date
        if (model.Date == default)
            errors.Add("Date is required.");
        else if (model.Date < DateTime.MinValue)
            errors.Add("Date must be a valid date.");

        // Validation for TemperatureC
        if (model.TemperatureC < -50 || model.TemperatureC > 50)
            errors.Add("TemperatureC must be between -50 and 50 degrees.");

        // Validation for FeelsLikeC
        if (model.FeelsLikeC < -50 || model.FeelsLikeC > 50)
            errors.Add("FeelsLikeC must be between -50 and 50 degrees.");

        // Validation for Humidity
        if (model.Humidity < 0 || model.Humidity > 100)
            errors.Add("Humidity must be between 0 and 100 percent.");

        // Validation for WindSpeedKph
        if (model.WindSpeedKph < 0 || model.WindSpeedKph > 300)
            errors.Add("WindSpeedKph must be between 0 and 300 km/h.");

        // Validation for WindDirection
        if (model.WindDirection < 0 || model.WindDirection > 360)
            errors.Add("WindDirection must be between 0 and 360 degrees.");

        // Validation for PressureHpa
        if (model.PressureHpa < 800 || model.PressureHpa > 1100)
            errors.Add("PressureHpa must be between 800 and 1100 hPa.");

        // Validation for PrecipitationMm
        if (model.PrecipitationMm < 0)
            errors.Add("PrecipitationMm cannot be negative.");

        // Validation for VisibilityKm
        if (model.VisibilityKm < 0 || model.VisibilityKm > 100)
            errors.Add("VisibilityKm must be between 0 and 100 km.");

        // Validation for UVIndex
        if (model.UVIndex < 0 || model.UVIndex > 11)
            errors.Add("UVIndex must be between 0 and 11.");

        // Validation for AirQualityIndex
        if (model.AirQualityIndex < 0)
            errors.Add("AirQualityIndex must be a positive value.");

        // Validation for CloudCover
        if (model.CloudCover < 0 || model.CloudCover > 100)
            errors.Add("CloudCover must be between 0 and 100 percent.");

        // Validation for LightningProbability
        if (model.LightningProbability < 0 || model.LightningProbability > 100)
            errors.Add("LightningProbability must be between 0 and 100 percent.");

        // Validation for PollenCount
        if (model.PollenCount < 0)
            errors.Add("PollenCount must be a positive value.");

        // Validation for Sunrise
        if (model.Sunrise < TimeSpan.Zero || model.Sunrise > new TimeSpan(23, 59, 59))
            errors.Add("Sunrise must be a valid time of day (00:00 - 23:59).");

        // Validation for Sunset
        if (model.Sunset < TimeSpan.Zero || model.Sunset > new TimeSpan(23, 59, 59))
            errors.Add("Sunset must be a valid time of day (00:00 - 23:59).");

        // Validation for Summary
        if (string.IsNullOrWhiteSpace(model.Summary))
            errors.Add("Summary is required.");
        else if (model.Summary.Length > ForecastLength.SummaryLength)
            errors.Add($"Summary {model.Summary} must not exceed {ForecastLength.SummaryLength} characters.");
        else if (model.Summary.ContainsInjectionCharacters())
            errors.Add($"Summary {model.Summary} contains invalid characters.");
        else if (model.Summary.ContainsNonPrintableCharacters())
            errors.Add($"Summary {model.Summary} contains non-printable characters.");

        // Validation for StationId
        if (model.StationId == Guid.Empty)
            errors.Add("StationId must be a valid GUID.");

        // Validation for WarningId
        if (model.WarningId == Guid.Empty)
            errors.Add("WarningId must be a valid GUID.");

        // Validation for MoonPhaseId
        if (model.MoonPhaseId == Guid.Empty)
            errors.Add("MoonPhaseId must be a valid GUID.");

        // Validation for Version
        if (model.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}