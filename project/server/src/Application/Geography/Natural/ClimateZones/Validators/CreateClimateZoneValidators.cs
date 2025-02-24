// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.ClimateZones.Dtos;

namespace server.src.Application.Geography.Natural.ClimateZones.Validators;

public static class CreateClimateZoneValidators
{
    /// <summary>
    /// Validates the properties of a <see cref="CreateClimateZoneDto"/>.
    /// Ensures that all required fields meet the expected constraints.
    /// </summary>
    /// <param name="dto">The DTO instance to validate.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating success or failure with error messages.</returns>
    public static ValidationResult Validate(this CreateClimateZoneDto dto)
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

        // Validation for AvgTemperatureC
        if (dto.AvgTemperatureC < -100 || dto.AvgTemperatureC > 100)
            errors.Add("AvgTemperatureC must be between -100 and 100 degrees Celsius.");

        // Validation for AvgPrecipitationMm
        if (dto.AvgPrecipitationMm < 0)
            errors.Add("AvgPrecipitationMm must be a non-negative value.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}