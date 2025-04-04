// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Regions.Dtos;
using server.src.Domain.Geography.Administrative.Regions.Extensions;

namespace server.src.Application.Geography.Administrative.Regions.Validators;

public static class UpdateRegionValidators
{
    public static ValidationResult Validate(this UpdateRegionDto dto)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");
        else if (dto.Name.Length > RegionSettings.NameLength)
            errors.Add($"Name {dto.Name} must not exceed {RegionSettings.NameLength} characters.");
        else if (dto.Name.ContainsInjectionCharacters())
            errors.Add($"Name {dto.Name} contains invalid characters.");
        else if (dto.Name.ContainsNonPrintableCharacters())
            errors.Add($"Name {dto.Name} contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(dto.Description))
            errors.Add("Description is required.");
        else if (dto.Description.Length > RegionSettings.DescriptionLength)
            errors.Add($"Description {dto.Description} must not exceed {RegionSettings.DescriptionLength} characters.");
        else if (dto.Description.ContainsInjectionCharacters())
            errors.Add($"Description {dto.Description} contains invalid characters.");
        else if (dto.Description.ContainsNonPrintableCharacters())
            errors.Add($"Description {dto.Description} contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(dto.Code))
            errors.Add("Code is required.");
        else if (dto.Code.Length > RegionSettings.CodeLength)
            errors.Add($"Code {dto.Code} must not exceed {RegionSettings.CodeLength} characters.");
        else if (dto.Code.ContainsInjectionCharacters())
            errors.Add($"Code {dto.Code} contains invalid characters.");
        else if (dto.Description.ContainsNonPrintableCharacters())
            errors.Add($"Code {dto.Code} contains non-printable characters.");


        // Validation for AreaKm2
        if (dto.AreaKm2 < 0)
            errors.Add("Area in square kilometers cannot be negative.");

        // Validation for StateId
        if (dto.StateId == Guid.Empty)
            errors.Add("StateId cannot be empty.");

        // Validation for Version
        if (dto.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}