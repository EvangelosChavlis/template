// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Collections.Warnings.Extensions;
using server.src.Domain.Weather.Collections.Warnings.Models;

namespace server.src.Application.Weather.Collections.Warnings.Validators;

public static class WarningModelValidators
{
    public static ValidationResult Validate(this Warning model)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(model.Name))
            errors.Add("Name is required.");
        else if (model.Name.Length > WarningLength.NameLength)
            errors.Add($"Name {model.Name} must not exceed {WarningLength.NameLength} characters.");
        else if (model.Name.ContainsInjectionCharacters())
            errors.Add($"Name {model.Name} contains invalid characters.");
        else if (model.Name.ContainsNonPrintableCharacters())
            errors.Add($"Name {model.Name} contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(model.Description))
            errors.Add("Description is required.");
        else if (model.Description.Length > WarningLength.DescriptionLength)
            errors.Add($"Description {model.Description} must not exceed {WarningLength.DescriptionLength} characters.");
        else if (model.Description.ContainsInjectionCharacters())
            errors.Add($"Description {model.Description} contains invalid characters.");
        else if (model.Description.ContainsNonPrintableCharacters())
            errors.Add($"Description {model.Description} contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(model.Code))
            errors.Add("Code is required.");
        else if (model.Code.Length > WarningLength.CodeLength)
            errors.Add($"Code {model.Code} must not exceed {WarningLength.CodeLength} characters.");
        else if (model.Code.ContainsInjectionCharacters())
            errors.Add($"Code {model.Code} contains invalid characters.");
        else if (model.Code.ContainsNonPrintableCharacters())
            errors.Add($"Code {model.Code} contains non-printable characters.");

        // Validation for RecommendedActions
        if (string.IsNullOrWhiteSpace(model.RecommendedActions))
            errors.Add("RecommendedActions is required.");
        else if (model.RecommendedActions.Length > WarningLength.RecommendedActionsLength)
            errors.Add($"RecommendedActions {model.RecommendedActions} must not exceed {WarningLength.RecommendedActionsLength} characters.");
        else if (model.RecommendedActions.ContainsInjectionCharacters())
            errors.Add($"RecommendedActions {model.RecommendedActions} contains invalid characters.");
        else if (model.RecommendedActions.ContainsNonPrintableCharacters())
            errors.Add($"RecommendedActions {model.RecommendedActions} contains non-printable characters.");

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
