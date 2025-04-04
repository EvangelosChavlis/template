// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;
using server.src.Domain.Geography.Natural.NaturalFeatures.Extensions;

namespace server.src.Application.Geography.Natural.NaturalFeatures.Validators;

public static class CreateNaturalFeatureValidators
{
    public static ValidationResult Validate(this CreateNaturalFeatureDto dto)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");
        else if (dto.Name.Length > NaturalFeatureSettings.NameLength)
            errors.Add($"Name {NaturalFeatureSettings.NameLength} must not exceed {NaturalFeatureSettings.NameLength} characters.");
        else if (dto.Name.ContainsInjectionCharacters())
            errors.Add($"Name {NaturalFeatureSettings.NameLength} contains invalid characters.");
        else if (dto.Name.ContainsNonPrintableCharacters())
            errors.Add($"Name {NaturalFeatureSettings.NameLength} contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(dto.Description))
            errors.Add("Description is required.");
        else if (dto.Description.Length > NaturalFeatureSettings.DescriptionLength)
            errors.Add($"Description {dto.Description} must not exceed {NaturalFeatureSettings.DescriptionLength} characters.");
        else if (dto.Description.ContainsInjectionCharacters())
            errors.Add($"Description {dto.Description} contains invalid characters.");
        else if (dto.Description.ContainsNonPrintableCharacters())
            errors.Add($"Description {dto.Description} contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(dto.Code))
            errors.Add("Code is required.");
        else if (dto.Code.Length > NaturalFeatureSettings.CodeLength)
            errors.Add($"Code {dto.Code} must not exceed {NaturalFeatureSettings.CodeLength} characters.");
        else if (dto.Code.ContainsInjectionCharacters())
            errors.Add($"Code {dto.Code} contains invalid characters.");
        else if (dto.Description.ContainsNonPrintableCharacters())
            errors.Add($"Code {dto.Code} contains non-printable characters.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}