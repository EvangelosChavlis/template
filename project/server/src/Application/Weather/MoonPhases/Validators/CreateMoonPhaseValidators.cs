// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.MoonPhases.Dtos;

namespace server.src.Application.Weather.MoonPhases.Validators;

public static class CreateMoonPhaseValidators
{
    public static ValidationResult Validate(this CreateMoonPhaseDto dto)
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

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}