// packages
using System.Text.RegularExpressions;

// source
using server.src.Application.Common.Validators;
using server.src.Domain.Auth.Users.Dtos;
using server.src.Domain.Models.Errors;

namespace server.src.Application.Auth.Users.Validators;

public static class CreateUserValidators
{
    public static ValidationResult Validate(CreateUserDto dto)
    {
        var errors = new List<string>();

        // Validation for FirstName
        if (string.IsNullOrWhiteSpace(dto.FirstName))
            errors.Add("First name is required.");
        else if (dto.FirstName.Length > 50)
            errors.Add("First name must not exceed 50 characters.");
        else if (dto.FirstName.ContainsInjectionCharacters())
            errors.Add("First name contains invalid characters.");
        else if (dto.FirstName.ContainsNonPrintableCharacters())
             errors.Add("First name contains non-printable characters.");
            
        // Validation for LastName
        if (string.IsNullOrWhiteSpace(dto.LastName))
            errors.Add("Last name is required.");
        else if (dto.LastName.Length > 50)
            errors.Add("Last name must not exceed 50 characters.");
        else if (dto.LastName.ContainsInjectionCharacters())
            errors.Add("Last name contains invalid characters.");
        else if (dto.FirstName.ContainsNonPrintableCharacters())
            errors.Add("Last name contains non-printable characters.");

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
        else if (dto.LastName.ContainsNonPrintableCharacters())
            errors.Add("User name contains invalid characters.");

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
}