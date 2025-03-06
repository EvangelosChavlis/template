// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Countries.Extensions;
using server.src.Domain.Geography.Administrative.Countries.Models;

namespace server.src.Application.Geography.Administrative.Countries.Validators;

public static class CountryModelValidators
{
    public static ValidationResult Validate(this Country model)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(model.Name))
            errors.Add("Name is required.");
        else if (model.Name.Length > CountryLength.NameLength)
            errors.Add($"Name must not exceed {CountryLength.NameLength} characters.");
        else if (model.Name.ContainsInjectionCharacters())
            errors.Add("Name contains invalid characters.");
        else if (model.Name.ContainsNonPrintableCharacters())
            errors.Add("Name contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(model.Description))
            errors.Add("Description is required.");
        else if (model.Description.Length > CountryLength.DescriptionLength)
            errors.Add($"Description must not exceed {CountryLength.DescriptionLength} characters.");
        else if (model.Description.ContainsInjectionCharacters())
            errors.Add("Description contains invalid characters.");
        else if (model.Description.ContainsNonPrintableCharacters())
            errors.Add("Description contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(model.Code))
            errors.Add("Code is required.");
        else if (model.Code.Length > CountryLength.CodeLength)
            errors.Add($"Code must not exceed {CountryLength.CodeLength} characters.");
        else if (model.Code.ContainsInjectionCharacters())
            errors.Add("Code contains invalid characters.");
        else if (model.Code.ContainsNonPrintableCharacters())
            errors.Add("Code contains non-printable characters.");

        // Validation for Population
        if (model.Population < 0)
            errors.Add("Population cannot be negative.");

        // Validation for AreaKm2
        if (model.AreaKm2 < 0)
            errors.Add("Area in square kilometers cannot be negative.");

        // Validation for IsActive (boolean)
        if (model.IsActive is not true && model.IsActive is not false)
            errors.Add("IsActive must be either true or false.");
        
        // Validation for ContinentId
        if (model.ContinentId == Guid.Empty)
            errors.Add("ContinentId cannot be empty.");

        // Validation for Version (ensure it's a valid GUID)
        if (model.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}