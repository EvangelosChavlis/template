// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Neighborhoods.Extensions;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;

namespace server.src.Application.Geography.Administrative.Neighborhoods.Validators;

public static class NeighborhoodModelValidators
{
    public static ValidationResult Validate(this Neighborhood model)
    {
        var errors = new List<string>();

       // Validation for Name
        if (string.IsNullOrWhiteSpace(model.Name))
            errors.Add("Name is required.");
        else if (model.Name.Length > NeighborhoodSettings.NameLength)
            errors.Add($"Name must not exceed {NeighborhoodSettings.NameLength} characters.");
        else if (model.Name.ContainsInjectionCharacters())
            errors.Add($"Name {model.Name} contains invalid characters.");
        else if (model.Name.ContainsNonPrintableCharacters())
            errors.Add($"Name {model.Name} contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(model.Description))
            errors.Add("Description is required.");
        else if (model.Description.Length > NeighborhoodSettings.DescriptionLength)
            errors.Add($"Description {model.Description} must not exceed {NeighborhoodSettings.DescriptionLength} characters.");
        else if (model.Description.ContainsInjectionCharacters())
            errors.Add($"Description {model.Description} contains invalid characters.");
        else if (model.Description.ContainsNonPrintableCharacters())
            errors.Add($"Description {model.Description} contains non-printable characters.");

        // Validation for Zipcode
        if (string.IsNullOrWhiteSpace(model.Zipcode))
            errors.Add("Zipcode is required.");
        else if (model.Zipcode.Length > NeighborhoodSettings.ZipCodeLength)
            errors.Add($"Zipcode {model.Zipcode} must not exceed {NeighborhoodSettings.ZipCodeLength} characters.");
        else if (model.Zipcode.ContainsInjectionCharacters())
            errors.Add($"Zipcode {model.Zipcode} contains invalid characters.");
        else if (model.Zipcode.ContainsNonPrintableCharacters())
            errors.Add($"Zipcode {model.Zipcode} contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(model.Code))
            errors.Add("Code is required.");
        else if (model.Code.Length > NeighborhoodSettings.CodeLength)
            errors.Add($"Code {model.Code} must not exceed {NeighborhoodSettings.CodeLength} characters.");
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
        
        // Validation for DistrictId
        if (model.DistrictId == Guid.Empty)
            errors.Add("DistrictId cannot be empty.");

        // Validation for Version (ensure it's a valid GUID)
        if (model.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}