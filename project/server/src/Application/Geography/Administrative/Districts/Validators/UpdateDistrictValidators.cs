// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Districts.Dtos;

namespace server.src.Application.Geography.Administrative.Districts.Validators;

public static class UpdateDistrictValidators
{
    public static ValidationResult Validate(this UpdateDistrictDto dto)
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

        // Validation for Population
        if (dto.Population < 0)
            errors.Add("Population cannot be negative.");

        // Validation for MunicipalityId
        if (dto.MunicipalityId == Guid.Empty)
            errors.Add("MunicipalityId cannot be empty.");

        // Validation for Version
        if (dto.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}