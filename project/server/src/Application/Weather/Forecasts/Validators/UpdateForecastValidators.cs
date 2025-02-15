using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Forecasts.Dtos;

namespace server.src.Application.Weather.Forecasts.Validators;

public static class UpdateForecastValidators
{
    public static ValidationResult Validate(this UpdateForecastDto dto)
    {
        var errors = new List<string>();

        // Validation for Date
        if (dto.Date == default)
            errors.Add("Date is required.");
        else if (dto.Date < DateTime.MinValue)
            errors.Add("Date must be a valid date.");

        // Validation for TemperatureC
        if (dto.TemperatureC < -50 || dto.TemperatureC > 50)
            errors.Add("TemperatureC must be between -50 and 50 degrees.");

        // Validation for FeelsLikeC
        if (dto.FeelsLikeC < -50 || dto.FeelsLikeC > 50)
            errors.Add("FeelsLikeC must be between -50 and 50 degrees.");

        // Validation for Humidity
        if (dto.Humidity < 0 || dto.Humidity > 100)
            errors.Add("Humidity must be between 0 and 100.");

        // Validation for WindSpeedKph
        if (dto.WindSpeedKph < 0)
            errors.Add("WindSpeedKph cannot be negative.");

        // Validation for WindDirection
        if (dto.WindDirection < 0 || dto.WindDirection > 360)
            errors.Add("WindDirection must be between 0 and 360 degrees.");

        // Validation for PressureHpa
        if (dto.PressureHpa <= 0)
            errors.Add("PressureHpa must be greater than 0.");

        // Validation for PrecipitationMm
        if (dto.PrecipitationMm < 0)
            errors.Add("PrecipitationMm cannot be negative.");

        // Validation for VisibilityKm
        if (dto.VisibilityKm < 0)
            errors.Add("VisibilityKm cannot be negative.");

        // Validation for UVIndex
        if (dto.UVIndex < 0)
            errors.Add("UVIndex cannot be negative.");

        // Validation for AirQualityIndex
        if (dto.AirQualityIndex < 0)
            errors.Add("AirQualityIndex cannot be negative.");

        // Validation for CloudCover
        if (dto.CloudCover < 0 || dto.CloudCover > 100)
            errors.Add("CloudCover must be between 0 and 100.");

        // Validation for LightningProbability
        if (dto.LightningProbability < 0 || dto.LightningProbability > 100)
            errors.Add("LightningProbability must be between 0 and 100.");

        // Validation for PollenCount
        if (dto.PollenCount < 0)
            errors.Add("PollenCount cannot be negative.");

        // Validation for Sunrise and Sunset
        if (dto.Sunrise == default)
            errors.Add("Sunrise is required.");
        if (dto.Sunset == default)
            errors.Add("Sunset is required.");

        // Validation for Summary
        if (string.IsNullOrWhiteSpace(dto.Summary))
            errors.Add("Summary is required.");
        else if (dto.Summary.Length > 200)
            errors.Add("Summary must not exceed 200 characters.");
        else if (dto.Summary.ContainsInjectionCharacters())
            errors.Add("Summary contains invalid characters.");
        else if (dto.Summary.ContainsNonPrintableCharacters())
            errors.Add("Summary contains non-printable characters.");

        // Validation for LocationId
        if (dto.LocationId == Guid.Empty)
            errors.Add("LocationId must be a valid GUID.");

        // Validation for WarningId
        if (dto.WarningId == Guid.Empty)
            errors.Add("WarningId must be a valid GUID.");

        // Validation for MoonPhaseId
        if (dto.MoonPhaseId == Guid.Empty)
            errors.Add("MoonPhaseId must be a valid GUID.");

        // Validation for Version
        if (dto.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}