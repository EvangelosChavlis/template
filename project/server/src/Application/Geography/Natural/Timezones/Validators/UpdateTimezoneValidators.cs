// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.Timezones.Dtos;
using server.src.Domain.Geography.Natural.Timezones.Extensions;

namespace server.src.Application.Geography.Natural.Timezones.Validators;

public static class UpdateTimezoneValidators
{
    public static ValidationResult Validate(this UpdateTimezoneDto dto)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");
        else if (dto.Name.Length > TimezoneSettings.NameLength)
            errors.Add($"Name {dto.Name} must not exceed {TimezoneSettings.NameLength} characters.");
        else if (dto.Name.ContainsInjectionCharacters())
            errors.Add($"Name {dto.Name} contains invalid characters.");
        else if (dto.Name.ContainsNonPrintableCharacters())
            errors.Add($"Name {dto.Name} contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(dto.Description))
            errors.Add("Description is required.");
        else if (dto.Description.Length > TimezoneSettings.DescriptionLength)
            errors.Add($"Description {dto.Description} must not exceed {TimezoneSettings.DescriptionLength} characters.");
        else if (dto.Description.ContainsInjectionCharacters())
            errors.Add($"Description {dto.Description} contains invalid characters.");
        else if (dto.Description.ContainsNonPrintableCharacters())
            errors.Add($"Description {dto.Description} contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(dto.Code))
            errors.Add("Code is required.");
        else if (dto.Code.Length > TimezoneSettings.CodeLength)
            errors.Add($"Code {dto.Code} must not exceed {TimezoneSettings.CodeLength} characters.");
        else if (dto.Code.ContainsInjectionCharacters())
            errors.Add($"Code {dto.Code} contains invalid characters.");
        else if (dto.Code.ContainsNonPrintableCharacters())
            errors.Add($"Code {dto.Code} contains non-printable characters.");

        // Validation for UtcOffset
        if (dto.UtcOffset < -12 || dto.UtcOffset > 14)
            errors.Add("UtcOffset must be between -12 and 14.");

        // Validation for SupportsDaylightSaving and DstOffset
        if (dto.SupportsDaylightSaving && dto.DstOffset == null)
            errors.Add("DstOffset is required when SupportsDaylightSaving is true.");

        // Validation for DstOffset (if applicable)
        if (dto.DstOffset.HasValue && (dto.DstOffset < -12 || dto.DstOffset > 14))
            errors.Add("DstOffset must be between -12 and 14.");

        // Validation for Version
        if (dto.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}