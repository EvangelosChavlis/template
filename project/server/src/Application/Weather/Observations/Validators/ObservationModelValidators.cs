// source
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Observations.Models;

namespace server.src.Application.Weather.Observations.Validators
{
    public static class ObservationModelValidators
    {
        public static ValidationResult Validate(this Observation model)
        {
            var errors = new List<string>();

            // Validation for Timestamp (Date)
            if (model.Timestamp == default)
                errors.Add("Timestamp is required.");
            else if (model.Timestamp < DateTime.MinValue)
                errors.Add("Timestamp must be a valid date.");

            // Validation for TemperatureC
            if (model.TemperatureC < -50 || model.TemperatureC > 50)
                errors.Add("TemperatureC must be between -50 and 50 degrees.");

            // Validation for Humidity
            if (model.Humidity < 0 || model.Humidity > 100)
                errors.Add("Humidity must be between 0 and 100.");

            // Validation for WindSpeedKph
            if (model.WindSpeedKph < 0)
                errors.Add("WindSpeedKph cannot be negative.");

            // Validation for WindDirection
            if (model.WindDirection < 0 || model.WindDirection > 360)
                errors.Add("WindDirection must be between 0 and 360 degrees.");

            // Validation for PressureHpa
            if (model.PressureHpa <= 0)
                errors.Add("PressureHpa must be greater than 0.");

            // Validation for PrecipitationMm
            if (model.PrecipitationMm < 0)
                errors.Add("PrecipitationMm cannot be negative.");

            // Validation for VisibilityKm
            if (model.VisibilityKm < 0)
                errors.Add("VisibilityKm cannot be negative.");

            // Validation for UVIndex
            if (model.UVIndex < 0)
                errors.Add("UVIndex cannot be negative.");

            // Validation for AirQualityIndex
            if (model.AirQualityIndex < 0)
                errors.Add("AirQualityIndex cannot be negative.");

            // Validation for CloudCover
            if (model.CloudCover < 0 || model.CloudCover > 100)
                errors.Add("CloudCover must be between 0 and 100.");

            // Validation for LightningProbability
            if (model.LightningProbability < 0 || model.LightningProbability > 100)
                errors.Add("LightningProbability must be between 0 and 100.");

            // Validation for PollenCount
            if (model.PollenCount < 0)
                errors.Add("PollenCount cannot be negative.");

            // Validation for MoonPhaseId
            if (model.MoonPhaseId == Guid.Empty)
                errors.Add("MoonPhaseId must be a valid GUID.");

            // Validation for LocationId
            if (model.LocationId == Guid.Empty)
                errors.Add("LocationId must be a valid GUID.");

            // Validation for Version (if it's part of the base class)
            if (model.Version == Guid.Empty)
                errors.Add("Invalid Version. The GUID must not be empty.");

            return errors.Count > 0 ? 
                ValidationResult.Failure(errors) : ValidationResult.Success();
        }
    }
}