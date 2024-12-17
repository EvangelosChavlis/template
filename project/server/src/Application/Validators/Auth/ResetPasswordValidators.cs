// packages
using FluentValidation;

// source
using server.src.Domain.Dto.Auth;

namespace server.src.Application.Validators.Auth;

public class ResetPasswordValidators : AbstractValidator<ResetPasswordDto>
{
    public ResetPasswordValidators()
    {
        RuleFor(r => r.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(r => r.Token)
            .NotNull()
            .NotEmpty()
            .WithMessage("Token is required.");

        RuleFor(r => r.NewPassword)
            .NotNull()
            .NotEmpty()
            .WithMessage("New password is required.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one number.")
            .Matches(@"[\W]").WithMessage("Password must contain at least one special character.");
    }
}
