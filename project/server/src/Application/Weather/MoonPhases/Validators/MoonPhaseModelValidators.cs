// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.MoonPhases.Models;

namespace server.src.Application.Weather.MoonPhases.Validators;

public static class MoonPhaseModelValidators
{
    public static ValidationResult Validate(this MoonPhase model)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(model.Name))
            errors.Add("Name is required.");
        else if (model.Name.Length > 100)
            errors.Add("Name must not exceed 100 characters.");
        else if (model.Name.ContainsInjectionCharacters())
            errors.Add("Name contains invalid characters.");
        else if (model.Name.ContainsNonPrintableCharacters())
            errors.Add("Name contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(model.Description))
            errors.Add("Description is required.");
        else if (model.Description.Length > 250)
            errors.Add("Description must not exceed 250 characters.");
        else if (model.Description.ContainsInjectionCharacters())
            errors.Add("Description contains invalid characters.");
        else if (model.Description.ContainsNonPrintableCharacters())
            errors.Add("Description contains non-printable characters.");

        // Validation for IlluminationPercentage (0-100%)
        if (model.IlluminationPercentage < 0 || model.IlluminationPercentage > 100)
            errors.Add("Illumination percentage must be between 0 and 100.");

        // Validation for PhaseOrder (Must be positive)
        if (model.PhaseOrder <= 0)
            errors.Add("Phase order must be a positive integer.");

        // Validation for DurationDays (Must be positive)
        if (model.DurationDays <= 0)
            errors.Add("Duration in days must be a positive number.");

        // Validation for OccurrenceDate (Cannot be in the past)
        if (model.OccurrenceDate < DateTime.UtcNow.Date)
            errors.Add("Occurrence date cannot be in the past.");

        // Validation for Version
        if (model.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}