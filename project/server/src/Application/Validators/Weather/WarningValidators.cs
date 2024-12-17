// packages
using FluentValidation;

// source
using server.src.Domain.Dto.Weather;

namespace server.src.Application.Validators.Warning;

public class WarningValidators : AbstractValidator<WarningDto>
{
    public WarningValidators()
    {
        RuleFor(w => w.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage("Name must not exceed 100 characters.");

        RuleFor(w => w.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters.");
    }
}
