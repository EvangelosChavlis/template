// packages
using FluentValidation;

// source
using server.src.Domain.Dto.Auth;

namespace server.src.Application.Validators.Auth;

public class ForgotPasswordValidators : AbstractValidator<ForgotPasswordDto>
{
    public ForgotPasswordValidators()
    {
        RuleFor(f => f.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");
    }
}
