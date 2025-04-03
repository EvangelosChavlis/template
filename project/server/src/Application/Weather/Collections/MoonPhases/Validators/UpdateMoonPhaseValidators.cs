// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Collections.MoonPhases.Dtos;
using server.src.Domain.Weather.Collections.MoonPhases.Extensions;

namespace server.src.Application.Weather.Collections.MoonPhases.Validators;

public static class UpdateMoonPhaseValidators
{
    public static ValidationResult Validate(this UpdateMoonPhaseDto dto)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");
        else if (dto.Name.Length > MoonPhaseLength.NameLength)
            errors.Add($"Name {dto.Name} must not exceed {MoonPhaseLength.NameLength} characters.");
        else if (dto.Name.ContainsInjectionCharacters())
            errors.Add($"Name {dto.Name} contains invalid characters.");
        else if (dto.Name.ContainsNonPrintableCharacters())
            errors.Add($"Name {dto.Name} contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(dto.Description))
            errors.Add("Description is required.");
        else if (dto.Description.Length > MoonPhaseLength.DescriptionLength)
            errors.Add($"Description {dto.Description} must not exceed {MoonPhaseLength.DescriptionLength} characters.");
        else if (dto.Description.ContainsInjectionCharacters())
            errors.Add($"Description {dto.Description} contains invalid characters.");
        else if (dto.Description.ContainsNonPrintableCharacters())
            errors.Add($"Description {dto.Description} contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(dto.Code))
            errors.Add("Code is required.");
        else if (dto.Code.Length > MoonPhaseLength.CodeLength)
            errors.Add($"Code {dto.Code} must not exceed {MoonPhaseLength.CodeLength} characters.");
        else if (dto.Code.ContainsInjectionCharacters())
            errors.Add($"Code {dto.Code} contains invalid characters.");
        else if (dto.Code.ContainsNonPrintableCharacters())
            errors.Add($"Code {dto.Code} contains non-printable characters.");

        // Validation for IlluminationPercentage (0-100%)
        if (dto.IlluminationPercentage < 0 || dto.IlluminationPercentage > 100)
            errors.Add("Illumination percentage must be between 0 and 100.");

        // Validation for PhaseOrder (Must be positive)
        if (dto.PhaseOrder <= 0)
            errors.Add("Phase order must be a positive integer.");

        // Validation for DurationDays (Must be positive)
        if (dto.DurationDays <= 0)
            errors.Add("Duration in days must be a positive number.");

        // Validation for OccurrenceDate (Cannot be in the past)
        if (dto.OccurrenceDate < DateTime.UtcNow.Date)
            errors.Add("Occurrence date cannot be in the past.");

        // Validation for Version
        if (dto.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}
