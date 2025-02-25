// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
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

        // Validation for Zipcode
        if (string.IsNullOrWhiteSpace(model.Zipcode))
            errors.Add("Zipcode is required.");
        else if (model.Zipcode.Length > 50)
            errors.Add("Description must not exceed 50 characters.");
        else if (model.Zipcode.ContainsInjectionCharacters())
            errors.Add("Description contains invalid characters.");
        else if (model.Zipcode.ContainsNonPrintableCharacters())
            errors.Add("Description contains non-printable characters.");

        // Validation for Population
        if (model.Population < 0)
            errors.Add("Population cannot be negative.");

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