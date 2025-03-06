// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Countries.Dtos;
using server.src.Domain.Geography.Administrative.Countries.Extensions;

namespace server.src.Application.Geography.Administrative.Countries.Validators;

public static class CreateCountryValidators
{
    public static ValidationResult Validate(this CreateCountryDto dto)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");
        else if (dto.Name.Length > CountryLength.NameLength)
            errors.Add($"Name must not exceed {CountryLength.NameLength} characters.");
        else if (dto.Name.ContainsInjectionCharacters())
            errors.Add("Name contains invalid characters.");
        else if (dto.Name.ContainsNonPrintableCharacters())
            errors.Add("Name contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(dto.Description))
            errors.Add("Description is required.");
        else if (dto.Description.Length > CountryLength.DescriptionLength)
            errors.Add($"Description must not exceed {CountryLength.DescriptionLength} characters.");
        else if (dto.Description.ContainsInjectionCharacters())
            errors.Add("Description contains invalid characters.");
        else if (dto.Description.ContainsNonPrintableCharacters())
            errors.Add("Description contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(dto.Code))
            errors.Add("Code is required.");
        else if (dto.Code.Length > CountryLength.CodeLength)
            errors.Add($"Code must not exceed {CountryLength.CodeLength} characters.");
        else if (dto.Code.ContainsInjectionCharacters())
            errors.Add("Code contains invalid characters.");
        else if (dto.Code.ContainsNonPrintableCharacters())
            errors.Add("Code contains non-printable characters.");

        // Validation for Population
        if (dto.Population < 0)
            errors.Add("Population cannot be negative.");

        // Validation for AreaKm2
        if (dto.AreaKm2 < 0)
            errors.Add("Area in square kilometers cannot be negative.");

        // Validation for ContinentId
        if (dto.ContinentId == Guid.Empty)
            errors.Add("ContinentId cannot be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}