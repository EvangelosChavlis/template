// sourc
using server.src.Application.Common.Validators;
using server.src.Domain.Auth.Users.Dtos;
using server.src.Domain.Common.Models;

namespace server.src.Application.Auth.UserLogins.Validators;

public static class UserLoginDtoValidators
{
    public static ValidationResult Validate(UserLoginDto dto)
    {
        var errors = new List<string>();

        // Validation for Username
        if (string.IsNullOrWhiteSpace(dto.Username))
            errors.Add("Username is required.");
        else if (dto.Username.ContainsInjectionCharacters())
            errors.Add("Username contains invalid characters.");
        else if (dto.Username.ContainsNonPrintableCharacters())
            errors.Add("Username contains non-printable characters.");

        // Validation for Password
        if (string.IsNullOrWhiteSpace(dto.Password))
            errors.Add("Password is required.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}