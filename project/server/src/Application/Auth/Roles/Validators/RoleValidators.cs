// source
using server.src.Application.Common.Validators;
using server.src.Domain.Auth.Roles.Models;
using server.src.Domain.Models.Errors;

namespace server.src.Application.Auth.Roles.Validators;

public static class RoleValidators
{
    public static ValidationResult Validate(this Role model)
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

        // Validation for Normalized Name
        if (string.IsNullOrWhiteSpace(model.NormalizedName))
            errors.Add("NormalizedName is required.");
        else if (model.NormalizedName.Length > 100)
            errors.Add("NormalizedName must not exceed 100 characters.");
        else if (!model.NormalizedName.All(char.IsUpper))
            errors.Add("NormalizedName must be in capital letters.");
        else if (model.NormalizedName.ContainsInjectionCharacters())
            errors.Add("NormalizedName contains invalid characters.");
        else if (model.NormalizedName.ContainsNonPrintableCharacters())
            errors.Add("NormalizedName contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(model.Description))
            errors.Add("Description is required.");
        else if (model.Description.Length > 250)
            errors.Add("Description must not exceed 250 characters.");
        else if (model.Description.ContainsInjectionCharacters())
            errors.Add("Description contains invalid characters.");
        else if (model.Description.ContainsNonPrintableCharacters())
            errors.Add("Description contains non-printable characters.");

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
