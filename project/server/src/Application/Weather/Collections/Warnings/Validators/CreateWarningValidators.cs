// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Collections.Warnings.Dtos;
using server.src.Domain.Weather.Collections.Warnings.Extensions;

namespace server.src.Application.Weather.Collections.Warnings.Validators;

public static class CreateWarningValidators
{
    public static ValidationResult Validate(this CreateWarningDto dto)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");
        else if (dto.Name.Length > WarningLength.NameLength)
            errors.Add($"Name {dto.Name} must not exceed {WarningLength.NameLength} characters.");
        else if (dto.Name.ContainsInjectionCharacters())
            errors.Add($"Name {dto.Name} contains invalid characters.");
        else if (dto.Name.ContainsNonPrintableCharacters())
            errors.Add($"Name {dto.Name} contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(dto.Description))
            errors.Add("Description is required.");
        else if (dto.Description.Length > WarningLength.DescriptionLength)
            errors.Add($"Description {dto.Description} must not exceed {WarningLength.DescriptionLength} characters.");
        else if (dto.Description.ContainsInjectionCharacters())
            errors.Add($"Description {dto.Description} contains invalid characters.");
        else if (dto.Description.ContainsNonPrintableCharacters())
            errors.Add($"Description {dto.Description} contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(dto.Code))
            errors.Add("Code is required.");
        else if (dto.Code.Length > WarningLength.CodeLength)
            errors.Add($"Code {dto.Code} must not exceed {WarningLength.CodeLength} characters.");
        else if (dto.Code.ContainsInjectionCharacters())
            errors.Add($"Code {dto.Code} contains invalid characters.");
        else if (dto.Code.ContainsNonPrintableCharacters())
            errors.Add($"Code {dto.Code} contains non-printable characters.");

        // Validation for RecommendedActions
        if (string.IsNullOrWhiteSpace(dto.RecommendedActions))
            errors.Add("RecommendedActions is required.");
        else if (dto.RecommendedActions.Length > WarningLength.RecommendedActionsLength)
            errors.Add($"RecommendedActions {dto.RecommendedActions} must not exceed {WarningLength.RecommendedActionsLength} characters.");
        else if (dto.RecommendedActions.ContainsInjectionCharacters())
            errors.Add($"RecommendedActions {dto.RecommendedActions} contains invalid characters.");
        else if (dto.RecommendedActions.ContainsNonPrintableCharacters())
            errors.Add($"RecommendedActions {dto.RecommendedActions} contains non-printable characters.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}