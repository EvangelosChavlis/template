// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Continents.Extensions;
using server.src.Domain.Geography.Administrative.Continents.Models;

namespace server.src.Application.Geography.Administrative.Continents.Validators;

public static class ContinentModelValidators
{
    public static ValidationResult Validate(this Continent model)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(model.Name))
            errors.Add("Name is required.");
        else if (model.Name.Length > ContinentSettings.NameLength)
            errors.Add($"Name {model.Name} must not exceed {ContinentSettings.NameLength} characters.");
        else if (model.Name.ContainsInjectionCharacters())
            errors.Add($"Name {model.Name} contains invalid characters.");
        else if (model.Name.ContainsNonPrintableCharacters())
            errors.Add($"Name {model.Name} contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(model.Code))
            errors.Add("Code is required.");
        else if (model.Code.Length > ContinentSettings.CodeLength)
            errors.Add($"Code {model.Code} must not exceed {ContinentSettings.CodeLength} characters.");
        else if (model.Code.ContainsInjectionCharacters())
            errors.Add($"Code {model.Code} contains invalid characters.");
        else if (model.Code.ContainsNonPrintableCharacters())
            errors.Add($"Code {model.Code} contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(model.Description))
            errors.Add("Description is required.");
        else if (model.Description.Length > ContinentSettings.DescriptionLength)
            errors.Add($"Description {model.Description} must not exceed {ContinentSettings.DescriptionLength} characters.");
        else if (model.Description.ContainsInjectionCharacters())
            errors.Add($"Description {model.Description} contains invalid characters.");
        else if (model.Description.ContainsNonPrintableCharacters())
            errors.Add($"Description {model.Description} contains non-printable characters.");

        // Validation for Population
        if (model.Population < 0)
            errors.Add("Population cannot be negative.");

        // Validation for AreaKm2
        if (model.AreaKm2 < 0)
            errors.Add("Area in square kilometers cannot be negative.");

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