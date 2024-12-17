// packages
using FluentValidation;

// source
using server.src.Domain.Dto.Auth;

namespace server.src.Application.Validators.Auth;

public class UserValidators : AbstractValidator<UserDto>
{
    public UserValidators()
    {
        RuleFor(u => u.FirstName)
            .NotNull()
            .NotEmpty()
            .WithMessage("First name is required.")
            .MaximumLength(50)
            .WithMessage("First name must not exceed 50 characters.");

        RuleFor(u => u.LastName)
            .NotNull()
            .NotEmpty()
            .WithMessage("Last name is required.")
            .MaximumLength(50)
            .WithMessage("Last name must not exceed 50 characters.");

        RuleFor(u => u.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(u => u.UserName)
            .NotNull()
            .NotEmpty()
            .WithMessage("Username is required.")
            .MaximumLength(50)
            .WithMessage("Username must not exceed 50 characters.");

        RuleFor(u => u.Password)
            .NotNull()
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one number.")
            .Matches(@"[\W]").WithMessage("Password must contain at least one special character.");

        RuleFor(u => u.Address)
            .NotNull()
            .NotEmpty()
            .WithMessage("Address is required.");

        RuleFor(u => u.ZipCode)
            .NotNull()
            .NotEmpty()
            .WithMessage("ZipCode is required.")
            .Matches(@"^\d{5}(-\d{4})?$")
            .WithMessage("Invalid ZipCode format.");

        RuleFor(u => u.City)
            .NotNull()
            .NotEmpty()
            .WithMessage("City is required.");

        RuleFor(u => u.State)
            .NotNull()
            .NotEmpty()
            .WithMessage("State is required.");

        RuleFor(u => u.Country)
            .NotNull()
            .NotEmpty()
            .WithMessage("Country is required.");

        RuleFor(u => u.PhoneNumber)
            .NotNull()
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .Matches(@"^\+?\d{10,15}$")
            .WithMessage("Phone number must be a valid international format.");

        RuleFor(u => u.MobilePhoneNumber)
            .NotNull()
            .NotEmpty()
            .WithMessage("Mobile phone number is required.")
            .Matches(@"^\+?\d{10,15}$")
            .WithMessage("Mobile phone number must be a valid international format.");

        RuleFor(u => u.Bio)
            .MaximumLength(500)
            .WithMessage("Bio must not exceed 500 characters.");

        RuleFor(u => u.DateOfBirth)
            .NotEmpty()
            .WithMessage("Date of birth is required.")
            .LessThan(DateTime.Now.AddYears(-18))
            .WithMessage("User must be at least 18 years old.");
    }
}
