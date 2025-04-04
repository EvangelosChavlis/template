// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.SurfaceTypes.Extensions;
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;

namespace server.src.Application.Geography.Natural.SurfaceTypes.Validators;

public static class SurfaceTypeModelValidators
{
    public static ValidationResult Validate(this SurfaceType model)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(model.Name))
            errors.Add("Name is required.");
        else if (model.Name.Length > SurfaceTypeSettings.NameLength)
            errors.Add($"Name {SurfaceTypeSettings.NameLength} must not exceed {SurfaceTypeSettings.NameLength} characters.");
        else if (model.Name.ContainsInjectionCharacters())
            errors.Add($"Name {SurfaceTypeSettings.NameLength} contains invalid characters.");
        else if (model.Name.ContainsNonPrintableCharacters())
            errors.Add($"Name {SurfaceTypeSettings.NameLength} contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(model.Description))
            errors.Add("Description is required.");
        else if (model.Description.Length > SurfaceTypeSettings.DescriptionLength)
            errors.Add($"Description {model.Description} must not exceed {SurfaceTypeSettings.DescriptionLength} characters.");
        else if (model.Description.ContainsInjectionCharacters())
            errors.Add($"Description {model.Description} contains invalid characters.");
        else if (model.Description.ContainsNonPrintableCharacters())
            errors.Add($"Description {model.Description} contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(model.Code))
            errors.Add("Code is required.");
        else if (model.Code.Length > SurfaceTypeSettings.DescriptionLength)
            errors.Add($"Code {model.Description} must not exceed {SurfaceTypeSettings.DescriptionLength} characters.");
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
