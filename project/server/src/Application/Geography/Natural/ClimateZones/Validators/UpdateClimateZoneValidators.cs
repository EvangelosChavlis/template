// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.ClimateZones.Dtos;
using server.src.Domain.Geography.Natural.ClimateZones.Extensions;

namespace server.src.Application.Geography.Natural.ClimateZones.Validators;

public static class UpdateClimateZoneValidators
{
    /// <summary>
    /// Validates the properties of an <see cref="UpdateClimateZoneDto"/> instance.  
    /// Ensures that all fields conform to the expected data integrity rules.
    /// </summary>
    /// <param name="dto">The DTO instance to validate.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating success or failure with error messages.</returns>
    public static ValidationResult Validate(this UpdateClimateZoneDto dto)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");
        else if (dto.Name.Length > ClimateZoneLength.NameLength)
            errors.Add($"Name {dto.Name} must not exceed {ClimateZoneLength.NameLength} characters.");
        else if (dto.Name.ContainsInjectionCharacters())
            errors.Add($"Name {dto.Name} contains invalid characters.");
        else if (dto.Name.ContainsNonPrintableCharacters())
            errors.Add($"Name {dto.Name} contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(dto.Description))
            errors.Add("Description is required.");
        else if (dto.Description.Length > ClimateZoneLength.DescriptionLength)
            errors.Add($"Description {dto.Description} must not exceed {ClimateZoneLength.DescriptionLength} characters.");
        else if (dto.Description.ContainsInjectionCharacters())
            errors.Add($"Description {dto.Description} contains invalid characters.");
        else if (dto.Description.ContainsNonPrintableCharacters())
            errors.Add($"Description {dto.Description} contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(dto.Code))
            errors.Add("Code is required.");
        else if (dto.Code.Length > ClimateZoneLength.CodeLength)
            errors.Add($"Code {dto.Code} must not exceed {ClimateZoneLength.CodeLength} characters.");
        else if (dto.Code.ContainsInjectionCharacters())
            errors.Add($"Code {dto.Code} contains invalid characters.");
        else if (dto.Code.ContainsNonPrintableCharacters())
            errors.Add($"Code {dto.Code} contains non-printable characters.");

        // Validation for AvgTemperatureC
        if (dto.AvgTemperatureC < -100 || dto.AvgTemperatureC > 100)
            errors.Add("AvgTemperatureC must be between -100 and 100 degrees Celsius.");

        // Validation for AvgPrecipitationMm
        if (dto.AvgPrecipitationMm < 0)
            errors.Add("AvgPrecipitationMm must be a non-negative value.");

        // Validation for Version (Ensure it's a valid GUID)
        if (dto.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}