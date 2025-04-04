// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Districts.Dtos;
using server.src.Domain.Geography.Administrative.Districts.Extensions;

namespace server.src.Application.Geography.Administrative.Districts.Validators;

public static class UpdateDistrictValidators
{
    public static ValidationResult Validate(this UpdateDistrictDto dto)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");
        else if (dto.Name.Length > DistrictSettings.NameLength)
            errors.Add($"Name {dto.Name} must not exceed {DistrictSettings.NameLength} characters.");
        else if (dto.Name.ContainsInjectionCharacters())
            errors.Add($"Name {dto.Name} contains invalid characters.");
        else if (dto.Name.ContainsNonPrintableCharacters())
            errors.Add($"Name {dto.Name} contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(dto.Description))
            errors.Add("Description is required.");
        else if (dto.Description.Length > DistrictSettings.DescriptionLength)
            errors.Add($"Description {dto.Description} must not exceed {DistrictSettings.DescriptionLength} characters.");
        else if (dto.Description.ContainsInjectionCharacters())
            errors.Add($"Description {dto.Description} contains invalid characters.");
        else if (dto.Description.ContainsNonPrintableCharacters())
            errors.Add($"Description {dto.Description} contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(dto.Code))
            errors.Add("Code is required.");
        else if (dto.Code.Length > DistrictSettings.CodeLength)
            errors.Add($"Code {dto.Code} must not exceed {DistrictSettings.CodeLength} characters.");
        else if (dto.Code.ContainsInjectionCharacters())
            errors.Add($"Code {dto.Code} contains invalid characters.");
        else if (dto.Code.ContainsNonPrintableCharacters())
            errors.Add($"Code {dto.Code} contains non-printable characters.");
            
        // Validation for Population
        if (dto.Population < 0)
            errors.Add("Population cannot be negative.");

        // Validation for AreaKm2
        if (dto.AreaKm2 < 0)
            errors.Add("AreaKm2 cannot be negative.");


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