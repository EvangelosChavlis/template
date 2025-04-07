// source
using server.src.Application.Common.Validators;
using server.src.Domain.Auth.Roles.Extensions;
using server.src.Domain.Auth.Roles.Models;
using server.src.Domain.Common.Models;

namespace server.src.Application.Auth.Roles.Validators;

public static class RoleModelValidators
{
    public static ValidationResult Validate(this Role model)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(model.Name))
            errors.Add("Name is required.");
        else if (model.Name.Length > RoleSettings.NameLength)
            errors.Add($"Name {model.Name} must not exceed {RoleSettings.NameLength} characters.");
        else if (model.Name.ContainsInjectionCharacters())
            errors.Add($"Name {model.Name} contains invalid characters.");
        else if (model.Name.ContainsNonPrintableCharacters())
            errors.Add($"Name {model.Name} contains non-printable characters.");

        // Validation for Normalized Name
        if (string.IsNullOrWhiteSpace(model.NormalizedName))
            errors.Add("NormalizedName is required.");
        else if (model.NormalizedName.Length > RoleSettings.NormalizedName)
            errors.Add($"NormalizedName {model.NormalizedName} must not exceed {RoleSettings.NormalizedName} characters.");
        else if (!model.NormalizedName.All(char.IsUpper))
            errors.Add($"NormalizedName {model.NormalizedName} must be in capital letters.");
        else if (model.NormalizedName.ContainsInjectionCharacters())
            errors.Add($"NormalizedName {model.NormalizedName} contains invalid characters.");
        else if (model.NormalizedName.ContainsNonPrintableCharacters())
            errors.Add($"NormalizedName {model.NormalizedName} contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(model.Description))
            errors.Add("Description is required.");
        else if (model.Description.Length > RoleSettings.Description)
            errors.Add($"Description {model.Description} must not exceed {RoleSettings.Description} characters.");
        else if (model.Description.ContainsInjectionCharacters())
            errors.Add($"Description {model.Description} contains invalid characters.");
        else if (model.Description.ContainsNonPrintableCharacters())
            errors.Add($"Description {model.Description} contains non-printable characters.");

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
