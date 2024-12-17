// packages
using FluentValidation.TestHelper;

// source
using server.src.Application.Validators.Auth;
using server.src.Domain.Dto.Auth;

namespace server.test.Application.Unit.Tests.Validators.Auth;

public class ForgotPasswordValidatorTests
{
    private readonly ForgotPasswordValidators _validator;

    public ForgotPasswordValidatorTests()
    {
        _validator = new ForgotPasswordValidators();
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Null()
    {
        // Arrange
        var dto = new ForgotPasswordDto(null!);

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(f => f.Email)
            .WithErrorMessage("Email is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        // Arrange
        var dto = new ForgotPasswordDto(string.Empty);

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(f => f.Email)
            .WithErrorMessage("Email is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        // Arrange
        var dto = new ForgotPasswordDto("invalid-email");

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(f => f.Email)
            .WithErrorMessage("Invalid email format.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Email_Is_Valid()
    {
        // Arrange
        var dto = new ForgotPasswordDto("valid.email@example.com");

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldNotHaveValidationErrorFor(f => f.Email);
    }
}