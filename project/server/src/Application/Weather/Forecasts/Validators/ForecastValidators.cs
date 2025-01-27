// source
using server.src.Domain.Dto.Weather;
using server.src.Domain.Models.Weather;
using server.src.Domain.Models.Errors;

namespace server.src.Application.Weather.Forecasts.Validators;

public static class ForecastValidators
{
    public static ValidationResult Validate(ForecastDto dto)
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

        // Validation for Summary
        if (string.IsNullOrWhiteSpace(dto.Summary))
            errors.Add("Summary is required.");
        else if (dto.Summary.Length > 200)
            errors.Add("Summary must not exceed 200 characters.");
        else if (ContainsInjectionCharacters(dto.Summary))
            errors.Add("Summary contains invalid characters.");
        else if (ContainsNonPrintableCharacters(dto.Summary))
            errors.Add("Summary contains non-printable characters.");

        // Validation for WarningId
        if (dto.WarningId == Guid.Empty)
            errors.Add("WarningId must be a valid GUID.");

        // Validation for Version
        if (dto.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        return errors.Count > 0 ? ValidationResult.Failure(errors) : ValidationResult.Success();
    }

    public static ValidationResult Validate(Forecast model)
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

        // Validation for Summary
        if (string.IsNullOrWhiteSpace(model.Summary))
            errors.Add("Summary is required.");
        else if (model.Summary.Length > 200)
            errors.Add("Summary must not exceed 200 characters.");
        else if (ContainsInjectionCharacters(model.Summary))
            errors.Add("Summary contains invalid characters.");
        else if (ContainsNonPrintableCharacters(model.Summary))
            errors.Add("Summary contains non-printable characters.");

        // Validation for IsRead
        if (model.IsRead is not true && model.IsRead is not false)
            errors.Add("IsRead must be either true or false.");

        // Validation for Longitude & Latitude
        if (model.Longitude is < -180 or > 180)
            errors.Add("Longitude must be between -180 and 180.");
        if (model.Latitude is < -90 or > 90)
            errors.Add("Latitude must be between -90 and 90.");

        // Validation for Version
        if (model.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        // Validation for WarningId
        if (model.WarningId == Guid.Empty)
            errors.Add("WarningId must be a valid GUID.");

        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }

    public static ValidationResult Validate(Guid id)
    {
        var errors = new List<string>();

        // Validation for Id
        if (id == Guid.Empty)
            errors.Add("Invalid ID. The GUID must not be empty.");

        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }

    // Helper method to check for dangerous characters in input (SQL Injection prevention)
    private static bool ContainsInjectionCharacters(string input)
    {
        var dangerousChars = new[] { "'", ";", "--", "<", ">", "/*", "*/", "=", 
            "%", "@", "!", "#", "$", "^", "&" };
        return dangerousChars.Any(c => input.Contains(c));
    }

    // Helper method to check for non-printable characters (e.g., control characters)
    private static bool ContainsNonPrintableCharacters(string input)
    {
        return input.Any(char.IsControl);
    }
}
