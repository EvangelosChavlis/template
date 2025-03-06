// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Continents.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Extensions;

namespace server.src.Application.Geography.Administrative.Continents.Validators;

public static class CreateContinentValidators
{
    public static ValidationResult Validate(this CreateContinentDto dto)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");
        else if (dto.Name.Length > ContinentLength.NameLength)
            errors.Add($"Name must not exceed {ContinentLength.NameLength} characters.");
        else if (dto.Name.ContainsInjectionCharacters())
            errors.Add("Name contains invalid characters.");
        else if (dto.Name.ContainsNonPrintableCharacters())
            errors.Add("Name contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(dto.Code))
            errors.Add("Code is required.");
        else if (dto.Code.Length > ContinentLength.CodeLength)
            errors.Add($"Code must not exceed {ContinentLength.CodeLength} characters.");
        else if (dto.Code.ContainsInjectionCharacters())
            errors.Add("Code contains invalid characters.");
        else if (dto.Code.ContainsNonPrintableCharacters())
            errors.Add("Code contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(dto.Description))
            errors.Add("Description is required.");
        else if (dto.Description.Length > ContinentLength.DescriptionLength)
            errors.Add($"Description must not exceed {ContinentLength.DescriptionLength} characters.");
        else if (dto.Description.ContainsInjectionCharacters())
            errors.Add("Description contains invalid characters.");
        else if (dto.Description.ContainsNonPrintableCharacters())
            errors.Add("Description contains non-printable characters.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}
