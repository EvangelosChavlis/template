// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.NaturalFeatures.Extensions;
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;

namespace server.src.Application.Geography.Natural.NaturalFeatures.Validators;

public static class NaturalFeatureModelValidators
{
    public static ValidationResult Validate(this NaturalFeature model)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(model.Name))
            errors.Add("Name is required.");
        else if (model.Name.Length > NaturalFeatureSettings.NameLength)
            errors.Add($"Name {NaturalFeatureSettings.NameLength} must not exceed {NaturalFeatureSettings.NameLength} characters.");
        else if (model.Name.ContainsInjectionCharacters())
            errors.Add($"Name {NaturalFeatureSettings.NameLength} contains invalid characters.");
        else if (model.Name.ContainsNonPrintableCharacters())
            errors.Add($"Name {NaturalFeatureSettings.NameLength} contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(model.Description))
            errors.Add("Description is required.");
        else if (model.Description.Length > NaturalFeatureSettings.DescriptionLength)
            errors.Add($"Description {model.Description} must not exceed {NaturalFeatureSettings.DescriptionLength} characters.");
        else if (model.Description.ContainsInjectionCharacters())
            errors.Add($"Description {model.Description} contains invalid characters.");
        else if (model.Description.ContainsNonPrintableCharacters())
            errors.Add($"Description {model.Description} contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(model.Code))
            errors.Add("Code is required.");
        else if (model.Code.Length > NaturalFeatureSettings.CodeLength)
            errors.Add($"Code {model.Code} must not exceed {NaturalFeatureSettings.CodeLength} characters.");
        else if (model.Code.ContainsInjectionCharacters())
            errors.Add($"Code {model.Code} contains invalid characters.");
        else if (model.Description.ContainsNonPrintableCharacters())
            errors.Add($"Code {model.Code} contains non-printable characters.");

        // Validation for IsActive (boolean)
        if (model.IsActive is not true && model.IsActive is not false)
            errors.Add("IsActive must be either true or false.");

        // Validation for Version
        if (model.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}
