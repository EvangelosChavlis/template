// packages
using System.Text.RegularExpressions;

// source
using server.src.Domain.Common.Models;

namespace server.src.Application.Auth.Users.Validators;

public static class EmailValidators
{
    public static ValidationResult ValidateEmail(this string email)
    {
        var errors = new List<string>();

        // Validation for Email
        if (string.IsNullOrWhiteSpace(email))
            errors.Add("Email is required.");
        else if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            errors.Add("Invalid email format.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}