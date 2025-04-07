// source
using server.src.Application.Common.Validators;
using server.src.Domain.Auth.Roles.Dtos;
using server.src.Domain.Auth.Roles.Extensions;
using server.src.Domain.Common.Models;

namespace server.src.Application.Auth.Roles.Validators;

public static class CreateRoleValidators
{
    public static ValidationResult Validate(this CreateRoleDto dto)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");
        else if (dto.Name.Length > RoleSettings.NameLength)
            errors.Add($"Name {dto.Name} must not exceed {RoleSettings.NameLength} characters.");
        else if (dto.Name.ContainsInjectionCharacters())
            errors.Add($"Name {dto.Name} contains invalid characters.");
        else if (dto.Name.ContainsNonPrintableCharacters())
            errors.Add($"Name {dto.Name} contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(dto.Description))
            errors.Add("Description is required.");
        else if (dto.Description.Length > RoleSettings.Description)
            errors.Add($"Description {dto.Description} must not exceed {RoleSettings.Description} characters.");
        else if (dto.Description.ContainsInjectionCharacters())
            errors.Add($"Description {dto.Description} contains invalid characters.");
        else if (dto.Description.ContainsNonPrintableCharacters())
            errors.Add($"Description {dto.Description} contains non-printable characters.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}