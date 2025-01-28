// packages
using System.Text.RegularExpressions;

// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Models.Auth;
using server.src.Domain.Models.Errors;

namespace server.src.Application.Auth.Users.Validators;

public static class UserValidators
{
    public static ValidationResult Validate(UserDto dto)
    {
        var errors = new List<string>();

        // Validation for FirstName
        if (string.IsNullOrWhiteSpace(dto.FirstName))
            errors.Add("First name is required.");
        else if (dto.FirstName.Length > 50)
            errors.Add("First name must not exceed 50 characters.");
        else if (ContainsInjectionCharacters(dto.FirstName))
            errors.Add("First name contains invalid characters.");

        // Validation for LastName
        if (string.IsNullOrWhiteSpace(dto.LastName))
            errors.Add("Last name is required.");
        else if (dto.LastName.Length > 50)
            errors.Add("Last name must not exceed 50 characters.");
        else if (ContainsInjectionCharacters(dto.LastName))
            errors.Add("Last name contains invalid characters.");

        // Validation for Email
        if (string.IsNullOrWhiteSpace(dto.Email))
            errors.Add("Email is required.");
        else if (!Regex.IsMatch(dto.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            errors.Add("Invalid email format.");

        // Validation for UserName
        if (string.IsNullOrWhiteSpace(dto.UserName))
            errors.Add("Username is required.");
        else if (dto.UserName.Length > 50)
            errors.Add("Username must not exceed 50 characters.");

        // Validation for Password
        if (string.IsNullOrWhiteSpace(dto.Password))
            errors.Add("Password is required.");
        else if (dto.Password.Length < 8)
            errors.Add("Password must be at least 8 characters.");
        else if (!Regex.IsMatch(dto.Password, @"[A-Z]"))
            errors.Add("Password must contain at least one uppercase letter.");
        else if (!Regex.IsMatch(dto.Password, @"[a-z]"))
            errors.Add("Password must contain at least one lowercase letter.");
        else if (!Regex.IsMatch(dto.Password, @"\d"))
            errors.Add("Password must contain at least one number.");
        else if (!Regex.IsMatch(dto.Password, @"[\W_]"))
            errors.Add("Password must contain at least one special character.");

        // Validation for Address
        if (string.IsNullOrWhiteSpace(dto.Address))
            errors.Add("Address is required.");

        // Validation for ZipCode
        if (string.IsNullOrWhiteSpace(dto.ZipCode))
            errors.Add("ZipCode is required.");
        else if (!Regex.IsMatch(dto.ZipCode, @"^\d{5}(-\d{4})?$"))
            errors.Add("Invalid ZipCode format.");

        // Validation for City
        if (string.IsNullOrWhiteSpace(dto.City))
            errors.Add("City is required.");

        // Validation for State
        if (string.IsNullOrWhiteSpace(dto.State))
            errors.Add("State is required.");

        // Validation for Country
        if (string.IsNullOrWhiteSpace(dto.Country))
            errors.Add("Country is required.");

        // Validation for PhoneNumber
        if (string.IsNullOrWhiteSpace(dto.PhoneNumber))
            errors.Add("Phone number is required.");
        else if (!Regex.IsMatch(dto.PhoneNumber, @"^\+?\d{10,15}$"))
            errors.Add("Phone number must be a valid international format.");

        // Validation for MobilePhoneNumber
        if (string.IsNullOrWhiteSpace(dto.MobilePhoneNumber))
            errors.Add("Mobile phone number is required.");
        else if (!Regex.IsMatch(dto.MobilePhoneNumber, @"^\+?\d{10,15}$"))
            errors.Add("Mobile phone number must be a valid international format.");

        // Validation for Bio
        if (dto.Bio?.Length > 500)
            errors.Add("Bio must not exceed 500 characters.");

        // Validation for DateOfBirth
        if (dto.DateOfBirth == default)
            errors.Add("Date of birth is required.");
        else if (dto.DateOfBirth > DateTime.Now.AddYears(-18))
            errors.Add("User must be at least 18 years old.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }

    public static ValidationResult Validate(User model)
    {
        var errors = new List<string>();

        // Validation for FirstName
        if (string.IsNullOrWhiteSpace(model.FirstName))
            errors.Add("First name is required.");
        else if (model.FirstName.Length > 50)
            errors.Add("First name must not exceed 50 characters.");
        else if (ContainsInjectionCharacters(model.FirstName) || ContainsNonPrintableCharacters(model.FirstName))
            errors.Add("First name contains invalid characters.");

        // Validation for LastName
        if (string.IsNullOrWhiteSpace(model.LastName))
            errors.Add("Last name is required.");
        else if (model.LastName.Length > 50)
            errors.Add("Last name must not exceed 50 characters.");
        else if (ContainsInjectionCharacters(model.LastName) || ContainsNonPrintableCharacters(model.LastName))
            errors.Add("Last name contains invalid characters.");

        // Validation for Email
        if (string.IsNullOrWhiteSpace(model.Email))
            errors.Add("Email is required.");
        else if (!Regex.IsMatch(model.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            errors.Add("Invalid email format.");

        // Validation for UserName
        if (string.IsNullOrWhiteSpace(model.UserName))
            errors.Add("Username is required.");
        else if (model.UserName.Length > 50)
            errors.Add("Username must not exceed 50 characters.");

        // Validation for PasswordHash
        if (string.IsNullOrWhiteSpace(model.PasswordHash))
            errors.Add("Password is required.");

        // Validation for Address
        if (string.IsNullOrWhiteSpace(model.Address))
            errors.Add("Address is required.");

        // Validation for ZipCode
        if (string.IsNullOrWhiteSpace(model.ZipCode))
            errors.Add("Zip code is required.");
        else if (!Regex.IsMatch(model.ZipCode, @"^\d{5}(-\d{4})?$"))
            errors.Add("Invalid zip code format.");

        // Validation for PhoneNumber
        if (string.IsNullOrWhiteSpace(model.PhoneNumber))
            errors.Add("Phone number is required.");
        else if (!Regex.IsMatch(model.PhoneNumber, @"^\+?\d{10,15}$"))
            errors.Add("Invalid phone number format.");

        // Validation for MobilePhoneNumber
        if (string.IsNullOrWhiteSpace(model.MobilePhoneNumber))
            errors.Add("Mobile phone number is required.");
        else if (!Regex.IsMatch(model.MobilePhoneNumber, @"^\+?\d{10,15}$"))
            errors.Add("Invalid mobile phone number format.");

        // Validation for Bio
        if (!string.IsNullOrEmpty(model.Bio) && model.Bio.Length > 500)
            errors.Add("Bio must not exceed 500 characters.");

        // Validation for DateOfBirth
        if (model.DateOfBirth == default)
            errors.Add("Date of birth is required.");
        else if (model.DateOfBirth > DateTime.Now.AddYears(-18))
            errors.Add("User must be at least 18 years old.");

        // Validation for Version
        if (model.Version == Guid.Empty)
            errors.Add("Invalid version. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }

    public static ValidationResult Validate(Guid id)
    {
        var errors = new List<string>();

        // Validation for Id
        if (id == Guid.Empty)
            errors.Add("Invalid ID. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }

    public static ValidationResult ValidateEmail(string email)
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

    public static ValidationResult ValidateToken(string token)
    {
        var errors = new List<string>();

        // Validation for Email
        if (string.IsNullOrWhiteSpace(token))
            errors.Add("Email is required.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }

    public static ValidationResult ValidatePassword(string password)
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


    // Helper method to check for dangerous characters in input (SQL Injection prevention)
    private static bool ContainsInjectionCharacters(string input)
    {
        var dangerousChars = new[] { "'", ";", "--", "<", ">", 
            "/*", "*/", "=", "%", "@", "!", "#", "$", "^", "&" };
        return dangerousChars.Any(input.Contains);
    }

    // Helper method to check for non-printable characters (e.g., control characters)
    private static bool ContainsNonPrintableCharacters(string input)
    {
        return input.Any(char.IsControl);
    }
}
