// packages
using FluentValidation;

// source
using server.src.Domain.Dto.Auth;

namespace server.src.Application.Validators.Auth;

public class Verify2FAValidator : AbstractValidator<Verify2FADto>
{
    public Verify2FAValidator()
    {
        RuleFor(v => v.UserId)
            .NotNull()
            .NotEmpty()
            .WithMessage("UserId is required.");

        RuleFor(v => v.Token)
            .NotNull()
            .NotEmpty()
            .WithMessage("Token is required.");
    }
}
