// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;

namespace server.src.Application.Geography.Administrative.Neighborhoods.Validators;

public static class CreateNeighborhoodValidators
{
    public static ValidationResult Validate(this CreateNeighborhoodDto dto)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");
        else if (dto.Name.Length > 100)
            errors.Add("Name must not exceed 100 characters.");
        else if (dto.Name.ContainsInjectionCharacters())
            errors.Add("Name contains invalid characters.");
        else if (dto.Name.ContainsNonPrintableCharacters())
            errors.Add("Name contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(dto.Description))
            errors.Add("Description is required.");
        else if (dto.Description.Length > 250)
            errors.Add("Description must not exceed 250 characters.");
        else if (dto.Description.ContainsInjectionCharacters())
            errors.Add("Description contains invalid characters.");
        else if (dto.Description.ContainsNonPrintableCharacters())
            errors.Add("Description contains non-printable characters.");

        // Validation for Zipcode
        if (string.IsNullOrWhiteSpace(dto.Zipcode))
            errors.Add("Zipcode is required.");
        else if (dto.Zipcode.Length > 50)
            errors.Add("Description must not exceed 50 characters.");
        else if (dto.Zipcode.ContainsInjectionCharacters())
            errors.Add("Description contains invalid characters.");
        else if (dto.Zipcode.ContainsNonPrintableCharacters())
            errors.Add("Description contains non-printable characters.");

        // Validation for Population
        if (dto.Population < 0)
            errors.Add("Population cannot be negative.");

        // Validation for DistrictId
        if (dto.DistrictId == Guid.Empty)
            errors.Add("DistrictId cannot be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}