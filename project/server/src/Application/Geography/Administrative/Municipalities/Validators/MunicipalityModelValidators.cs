// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Municipalities.Extensions;
using server.src.Domain.Geography.Administrative.Municipalities.Models;

namespace server.src.Application.Geography.Administrative.Municipalities.Validators;

public static class MunicipalityModelValidators
{
    public static ValidationResult Validate(this Municipality model)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(model.Name))
            errors.Add("Name is required.");
        else if (model.Name.Length > MunicipalityLength.NameLength)
            errors.Add($"Name {model.Name} must not exceed {MunicipalityLength.NameLength} characters.");
        else if (model.Name.ContainsInjectionCharacters())
            errors.Add($"Name {model.Name} contains invalid characters.");
        else if (model.Name.ContainsNonPrintableCharacters())
            errors.Add($"Name {model.Name} contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(model.Description))
            errors.Add("Description is required.");
        else if (model.Description.Length > MunicipalityLength.DescriptionLength)
            errors.Add($"Description {model.Description} must not exceed {MunicipalityLength.DescriptionLength} characters.");
        else if (model.Description.ContainsInjectionCharacters())
            errors.Add($"Description {model.Description} contains invalid characters.");
        else if (model.Description.ContainsNonPrintableCharacters())
            errors.Add($"Description {model.Description} contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(model.Code))
            errors.Add("Description is required.");
        else if (model.Code.Length > MunicipalityLength.CodeLength)
            errors.Add($"Code {model.Code} must not exceed {MunicipalityLength.CodeLength} characters.");
        else if (model.Code.ContainsInjectionCharacters())
            errors.Add($"Code {model.Code} contains invalid characters.");
        else if (model.Code.ContainsNonPrintableCharacters())
            errors.Add($"Code {model.Code} contains non-printable characters.");

        // Validation for Population
        if (model.Population < 0)
            errors.Add("Population cannot be negative.");

        // Validation for AreaKm2
        if (model.AreaKm2 < 0)
            errors.Add("AreaKm2 cannot be negative.");

        // Validation for IsActive (boolean)
        if (model.IsActive is not true && model.IsActive is not false)
            errors.Add("IsActive must be either true or false.");
        
        // Validation for RegionId
        if (model.RegionId == Guid.Empty)
            errors.Add("RegionId cannot be empty.");

        // Validation for Version (ensure it's a valid GUID)
        if (model.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}