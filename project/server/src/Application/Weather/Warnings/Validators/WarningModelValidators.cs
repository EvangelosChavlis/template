// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Warnings.Models;

namespace server.src.Application.Weather.Warnings.Validators;

public static class WarningModelValidators
{
    public static ValidationResult Validate(this Warning model)
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

        // Validation for Version
        if (model.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}
