// packages
using FluentValidation;

// source
using server.src.Domain.Dto.Auth;

namespace server.src.Application.Validators.Auth;

public class UserLoginValidators : AbstractValidator<UserLoginDto>
{
    public UserLoginValidators()
    {
        RuleFor(u => u.Username)
            .NotNull()
            .NotEmpty()
            .WithMessage("Username is required.");

        RuleFor(u => u.Password)
            .NotNull()
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}
