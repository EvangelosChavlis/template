// source
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Observations.Dtos;

namespace server.src.Application.Weather.Observations.Validators;

public static class CreateObservationValidators
{
    public static ValidationResult Validate(this CreateObservationDto dto)
    {
        var errors = new List<string>();

        // Validation for Timestamp
        if (dto.Timestamp == default)
            errors.Add("Timestamp is required.");
        else if (dto.Timestamp < DateTime.MinValue)
            errors.Add("Timestamp must be a valid date.");

        // Validation for TemperatureC
        if (dto.TemperatureC < -50 || dto.TemperatureC > 50)
            errors.Add("TemperatureC must be between -50 and 50 degrees.");

        // Validation for Humidity
        if (dto.Humidity < 0 || dto.Humidity > 100)
            errors.Add("Humidity must be between 0 and 100 percent.");

        // Validation for WindSpeedKph
        if (dto.WindSpeedKph < 0 || dto.WindSpeedKph > 300)
            errors.Add("WindSpeedKph must be between 0 and 300 km/h.");

        // Validation for WindDirection
        if (dto.WindDirection < 0 || dto.WindDirection > 360)
            errors.Add("WindDirection must be between 0 and 360 degrees.");

        // Validation for PressureHpa
        if (dto.PressureHpa < 800 || dto.PressureHpa > 1100)
            errors.Add("PressureHpa must be between 800 and 1100 hPa.");

        // Validation for PrecipitationMm
        if (dto.PrecipitationMm < 0)
            errors.Add("PrecipitationMm cannot be negative.");

        // Validation for VisibilityKm
        if (dto.VisibilityKm < 0 || dto.VisibilityKm > 100)
            errors.Add("VisibilityKm must be between 0 and 100 km.");

        // Validation for UVIndex
        if (dto.UVIndex < 0 || dto.UVIndex > 11)
            errors.Add("UVIndex must be between 0 and 11.");

        // Validation for AirQualityIndex
        if (dto.AirQualityIndex < 0)
            errors.Add("AirQualityIndex must be a positive value.");

        // Validation for CloudCover
        if (dto.CloudCover < 0 || dto.CloudCover > 100)
            errors.Add("CloudCover must be between 0 and 100 percent.");

        // Validation for LightningProbability
        if (dto.LightningProbability < 0 || dto.LightningProbability > 100)
            errors.Add("LightningProbability must be between 0 and 100 percent.");

        // Validation for PollenCount
        if (dto.PollenCount < 0)
            errors.Add("PollenCount must be a positive value.");

        // Validation for LocationId
        if (dto.LocationId == Guid.Empty)
            errors.Add("LocationId must be a valid GUID.");

        // Validation for MoonPhaseId
        if (dto.MoonPhaseId == Guid.Empty)
            errors.Add("MoonPhaseId must be a valid GUID.");

        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}