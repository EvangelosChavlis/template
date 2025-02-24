using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.Timezones.Dtos;

namespace server.src.Application.Geography.Natural.Timezones.Validators;

public static class CreateTimezoneValidators
{
    public static ValidationResult Validate(this CreateTimezoneDto dto)
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

        // Validation for UtcOffset
        if (dto.UtcOffset < -12 || dto.UtcOffset > 14)
            errors.Add("UtcOffset must be between -12 and 14.");

        // Validation for SupportsDaylightSaving (additional check could be added for DST offset)
        if (dto.SupportsDaylightSaving && dto.DstOffset == null)
            errors.Add("DstOffset is required when SupportsDaylightSaving is true.");

        // Validation for DstOffset (if applicable)
        if (dto.DstOffset.HasValue && (dto.DstOffset < -12 || dto.DstOffset > 14))
            errors.Add("DstOffset must be between -12 and 14.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}
