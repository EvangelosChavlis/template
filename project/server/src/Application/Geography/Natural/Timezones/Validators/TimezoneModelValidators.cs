// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.Timezones.Extensions;
using server.src.Domain.Geography.Natural.Timezones.Models;

namespace server.src.Application.Geography.Natural.Timezones.Validators;

public static class TimezoneModelValidators
{
    public static ValidationResult Validate(this Timezone model)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(model.Name))
            errors.Add("Name is required.");
        else if (model.Name.Length > TimezoneSettings.NameLength)
            errors.Add($"Name {model.Name} must not exceed {TimezoneSettings.NameLength} characters.");
        else if (model.Name.ContainsInjectionCharacters())
            errors.Add($"Name {model.Name} contains invalid characters.");
        else if (model.Name.ContainsNonPrintableCharacters())
            errors.Add($"Name {model.Name} contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(model.Description))
            errors.Add("Description is required.");
        else if (model.Description.Length > TimezoneSettings.DescriptionLength)
            errors.Add($"Description {model.Description} must not exceed {TimezoneSettings.DescriptionLength} characters.");
        else if (model.Description.ContainsInjectionCharacters())
            errors.Add($"Description {model.Description} contains invalid characters.");
        else if (model.Description.ContainsNonPrintableCharacters())
            errors.Add($"Description {model.Description} contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(model.Code))
            errors.Add("Code is required.");
        else if (model.Code.Length > TimezoneSettings.CodeLength)
            errors.Add($"Code {model.Code} must not exceed {TimezoneSettings.CodeLength} characters.");
        else if (model.Code.ContainsInjectionCharacters())
            errors.Add($"Code {model.Code} contains invalid characters.");
        else if (model.Code.ContainsNonPrintableCharacters())
            errors.Add($"Code {model.Code} contains non-printable characters.");

        // Validation for UtcOffset
        if (model.UtcOffset < -12 || model.UtcOffset > 14)
            errors.Add("UtcOffset must be between -12 and 14.");

        // Validation for SupportsDaylightSaving and DstOffset
        if (model.SupportsDaylightSaving && model.DstOffset == null)
            errors.Add("DstOffset is required when SupportsDaylightSaving is true.");

        // Validation for DstOffset (if applicable)
        if (model.DstOffset.HasValue && (model.DstOffset < -12 || model.DstOffset > 14))
            errors.Add("DstOffset must be between -12 and 14.");

        // Validation for IsActive (boolean)
        if (model.IsActive is not true && model.IsActive is not false)
            errors.Add("IsActive must be either true or false.");
        
        // Validation for Version (ensure it's a valid GUID)
        if (model.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}