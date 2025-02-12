using System.Text.RegularExpressions;
using server.src.Domain.Common.Models;

namespace server.src.Application.Auth.Users.Validators;

public static class PasswordValidators
{
    public static ValidationResult ValidatePassword(this string password)
    {
        var errors = new List<string>();

        // Validation for Password
        if (string.IsNullOrWhiteSpace(password))
            errors.Add("Password is required.");
        else if (password.Length < 8)
            errors.Add("Password must be at least 8 characters.");
        else if (!Regex.IsMatch(password, @"[A-Z]"))
            errors.Add("Password must contain at least one uppercase letter.");
        else if (!Regex.IsMatch(password, @"[a-z]"))
            errors.Add("Password must contain at least one lowercase letter.");
        else if (!Regex.IsMatch(password, @"\d"))
            errors.Add("Password must contain at least one number.");
        else if (!Regex.IsMatch(password, @"[\W_]"))
            errors.Add("Password must contain at least one special character.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}