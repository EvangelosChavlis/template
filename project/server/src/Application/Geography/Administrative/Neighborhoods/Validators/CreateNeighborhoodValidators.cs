// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;
using server.src.Domain.Geography.Administrative.Neighborhoods.Extensions;

namespace server.src.Application.Geography.Administrative.Neighborhoods.Validators;

public static class CreateNeighborhoodValidators
{
    public static ValidationResult Validate(this CreateNeighborhoodDto dto)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");
        else if (dto.Name.Length > NeighborhoodLength.NameLength)
            errors.Add($"Name must not exceed {NeighborhoodLength.NameLength} characters.");
        else if (dto.Name.ContainsInjectionCharacters())
            errors.Add($"Name {dto.Name} contains invalid characters.");
        else if (dto.Name.ContainsNonPrintableCharacters())
            errors.Add($"Name {dto.Name} contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(dto.Description))
            errors.Add("Description is required.");
        else if (dto.Description.Length > NeighborhoodLength.DescriptionLength)
            errors.Add($"Description {dto.Description} must not exceed {NeighborhoodLength.DescriptionLength} characters.");
        else if (dto.Description.ContainsInjectionCharacters())
            errors.Add($"Description {dto.Description} contains invalid characters.");
        else if (dto.Description.ContainsNonPrintableCharacters())
            errors.Add($"Description {dto.Description} contains non-printable characters.");

        // Validation for Zipcode
        if (string.IsNullOrWhiteSpace(dto.Zipcode))
            errors.Add("Zipcode is required.");
        else if (dto.Zipcode.Length > NeighborhoodLength.ZipCodeLength)
            errors.Add($"Zipcode {dto.Zipcode} must not exceed {NeighborhoodLength.ZipCodeLength} characters.");
        else if (dto.Zipcode.ContainsInjectionCharacters())
            errors.Add($"Zipcode {dto.Zipcode} contains invalid characters.");
        else if (dto.Zipcode.ContainsNonPrintableCharacters())
            errors.Add($"Zipcode {dto.Zipcode} contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(dto.Code))
            errors.Add("Code is required.");
        else if (dto.Code.Length > NeighborhoodLength.CodeLength)
            errors.Add($"Code {dto.Code} must not exceed {NeighborhoodLength.CodeLength} characters.");
        else if (dto.Code.ContainsInjectionCharacters())
            errors.Add($"Code {dto.Code} contains invalid characters.");
        else if (dto.Code.ContainsNonPrintableCharacters())
            errors.Add($"Code {dto.Code} contains non-printable characters.");

        // Validation for Population
        if (dto.Population < 0)
            errors.Add("Population cannot be negative.");

        // Validation for AreaKm2
        if (dto.AreaKm2 < 0)
            errors.Add("AreaKm2 cannot be negative.");

        // Validation for DistrictId
        if (dto.DistrictId == Guid.Empty)
            errors.Add("DistrictId cannot be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}