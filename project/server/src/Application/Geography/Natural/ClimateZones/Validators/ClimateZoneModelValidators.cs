// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.ClimateZones.Extensions;
using server.src.Domain.Geography.Natural.ClimateZones.Models;

namespace server.src.Application.Geography.Natural.ClimateZones.Validators;

/// <summary>
/// Provides validation functionality for <see cref="ClimateZone"/> entities.  
/// Ensures that all required fields meet expected constraints.
/// </summary>
public static class ClimateZoneModelValidators
{
    /// <summary>
    /// Validates the properties of a <see cref="ClimateZone"/> instance.  
    /// Ensures that all fields conform to the expected data integrity rules.
    /// </summary>
    /// <param name="model">The ClimateZone model to validate.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating success or failure with error messages.</returns>
    public static ValidationResult Validate(this ClimateZone model)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(model.Name))
            errors.Add("Name is required.");
        else if (model.Name.Length > ClimateZoneLength.NameLength)
            errors.Add($"Name {model.Name} must not exceed {ClimateZoneLength.NameLength} characters.");
        else if (model.Name.ContainsInjectionCharacters())
            errors.Add($"Name {model.Name} contains invalid characters.");
        else if (model.Name.ContainsNonPrintableCharacters())
            errors.Add($"Name {model.Name} contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(model.Description))
            errors.Add("Description is required.");
        else if (model.Description.Length > ClimateZoneLength.DescriptionLength)
            errors.Add($"Description {model.Description} must not exceed {ClimateZoneLength.DescriptionLength} characters.");
        else if (model.Description.ContainsInjectionCharacters())
            errors.Add($"Description {model.Description} contains invalid characters.");
        else if (model.Description.ContainsNonPrintableCharacters())
            errors.Add($"Description {model.Description} contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(model.Code))
            errors.Add("Code is required.");
        else if (model.Code.Length > ClimateZoneLength.CodeLength)
            errors.Add($"Code {model.Code} must not exceed {ClimateZoneLength.CodeLength} characters.");
        else if (model.Code.ContainsInjectionCharacters())
            errors.Add($"Code {model.Code} contains invalid characters.");
        else if (model.Code.ContainsNonPrintableCharacters())
            errors.Add($"Code {model.Code} contains non-printable characters.");

        // Validation for AvgTemperatureC
        if (model.AvgTemperatureC < -100 || model.AvgTemperatureC > 100)
            errors.Add("AvgTemperatureC must be between -100 and 100 degrees Celsius.");

        // Validation for AvgPrecipitationMm
        if (model.AvgPrecipitationMm < 0)
            errors.Add("AvgPrecipitationMm must be a non-negative value.");

        // Validation for IsActive (Ensuring that it is explicitly set)
        if (model.IsActive != true && model.IsActive != false)
            errors.Add("IsActive must be explicitly set to true or false.");

        // Validation for Version (Ensure it's a valid GUID)
        if (model.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}