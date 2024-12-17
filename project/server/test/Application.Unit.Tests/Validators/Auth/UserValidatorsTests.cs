// source
using server.src.Application.Validators.Auth;
using server.src.Domain.Dto.Auth;

namespace server.test.Application.Unit.Tests.Validators.Auth;

public class UserValidatorsTests
{
    private readonly UserValidators _validator;

    public UserValidatorsTests()
    {
        _validator = new UserValidators();
    }

    [Fact]
    public void Should_PassValidation_When_ValidInput()
    {
        // Arrange
        var userDto = new UserDto(
            FirstName: "John",
            LastName: "Doe",
            Email: "john.doe@example.com",
            UserName: "johndoe123",
            Password: "P@ssw0rd123",
            Address: "123 Main St",
            ZipCode: "12345",
            City: "New York",
            State: "NY",
            Country: "USA",
            PhoneNumber: "+1234567890",
            MobilePhoneNumber: "+1234567890",
            Bio: "Bio of John Doe.",
            DateOfBirth: new DateTime(1990, 1, 1)
        );


        // Act
        var result = _validator.Validate(userDto);

        // Assert
        Assert.True(result.IsValid, "Validation should pass for valid input.");
    }

    [Fact]
    public void Should_FailValidation_When_FirstNameIsNull()
    {
        // Arrange
        var userDto = new UserDto(
            FirstName: null!,
            LastName: "Doe",
            Email: "john.doe@example.com",
            UserName: "johndoe123",
            Password: "P@ssw0rd123",
            Address: "123 Main St",
            ZipCode: "12345",
            City: "New York",
            State: "NY",
            Country: "USA",
            PhoneNumber: "+1234567890",
            MobilePhoneNumber: "+1234567890",
            Bio: "Bio of John Doe.",
            DateOfBirth: new DateTime(1990, 1, 1)
        );


        // Act
        var result = _validator.Validate(userDto);

        // Assert
        Assert.False(result.IsValid, "Validation should fail when FirstName is null.");
        Assert.Contains(result.Errors, error => error.PropertyName == "FirstName" && error.ErrorMessage == "First name is required.");
    }

    [Fact]
    public void Should_FailValidation_When_EmailIsInvalid()
    {
        // Arrange
        var userDto = new UserDto(
            FirstName: "John",
            LastName: "Doe",
            Email: "invalid-email",
            UserName: "johndoe123",
            Password: "P@ssw0rd123",
            Address: "123 Main St",
            ZipCode: "12345",
            City: "New York",
            State: "NY",
            Country: "USA",
            PhoneNumber: "+1234567890",
            MobilePhoneNumber: "+1234567890",
            Bio: "Bio of John Doe.",
            DateOfBirth: new DateTime(1990, 1, 1)
        );


        // Act
        var result = _validator.Validate(userDto);

        // Assert
        Assert.False(result.IsValid, "Validation should fail for an invalid email.");
        Assert.Contains(result.Errors, error => error.PropertyName == "Email" && error.ErrorMessage == "Invalid email format.");
    }

    [Fact]
    public void Should_FailValidation_When_PasswordDoesNotMeetCriteria()
    {
        // Arrange
        var userDto = new UserDto(
            FirstName: "John",
            LastName: "Doe",
            Email: "john.doe@example.com",
            UserName: "johndoe123",
            Password: "password", // Does not meet validation criteria
            Address: "123 Main St",
            ZipCode: "12345",
            City: "New York",
            State: "NY",
            Country: "USA",
            PhoneNumber: "+1234567890",
            MobilePhoneNumber: "+1234567890",
            Bio: "Bio of John Doe.",
            DateOfBirth: new DateTime(1990, 1, 1)
        );


        // Act
        var result = _validator.Validate(userDto);

        // Assert
        Assert.False(result.IsValid, "Validation should fail when password does not meet criteria.");
        Assert.Contains(result.Errors, error => error.PropertyName == "Password" && error.ErrorMessage.Contains("Password must contain at least one uppercase letter."));
        Assert.Contains(result.Errors, error => error.PropertyName == "Password" && error.ErrorMessage.Contains("Password must contain at least one special character."));
    }

    [Fact]
    public void Should_FailValidation_When_DateOfBirthIsUnder18()
    {
        // Arrange
        var userDto = new UserDto(
            FirstName: "John",
            LastName: "Doe",
            Email: "john.doe@example.com",
            UserName: "johndoe123",
            Password: "P@ssw0rd123",
            Address: "123 Main St",
            ZipCode: "12345",
            City: "New York",
            State: "NY",
            Country: "USA",
            PhoneNumber: "+1234567890",
            MobilePhoneNumber: "+1234567890",
            Bio: "Bio of John Doe.",
            DateOfBirth: DateTime.Now.AddYears(-17) // Less than 18 years old
        );


        // Act
        var result = _validator.Validate(userDto);

        // Assert
        Assert.False(result.IsValid, "Validation should fail for users under 18 years old.");
        Assert.Contains(result.Errors, error => error.PropertyName == "DateOfBirth" && error.ErrorMessage == "User must be at least 18 years old.");
    }

    [Fact]
    public void Should_FailValidation_When_BioExceedsMaxLength()
    {
        // Arrange
        var longBio = new string('A', 501); // 501 characters
        var userDto = new UserDto(
            FirstName: "John",
            LastName: "Doe",
            Email: "john.doe@example.com",
            UserName: "johndoe123",
            Password: "P@ssw0rd123",
            Address: "123 Main St",
            ZipCode: "12345",
            City: "New York",
            State: "NY",
            Country: "USA",
            PhoneNumber: "+1234567890",
            MobilePhoneNumber: "+1234567890",
            Bio: longBio, // Exceeds the maximum allowed length of 500 characters.
            DateOfBirth: new DateTime(1990, 1, 1)
        );

        // Act
        var result = _validator.Validate(userDto);

        // Assert
        Assert.False(result.IsValid, "Validation should fail when Bio exceeds 500 characters.");
        Assert.Contains(result.Errors, error => error.PropertyName == "Bio" && error.ErrorMessage == "Bio must not exceed 500 characters.");
    }

    [Fact]
    public void Should_FailValidation_When_PhoneNumberIsInvalid()
    {
        // Arrange
        var userDto = new UserDto(
            FirstName: "John",
            LastName: "Doe",
            Email: "john.doe@example.com",
            UserName: "johndoe123",
            Password: "P@ssw0rd123",
            Address: "123 Main St",
            ZipCode: "12345",
            City: "New York",
            State: "NY",
            Country: "USA",
            PhoneNumber: "invalid-phone", // Invalid phone number
            MobilePhoneNumber: "+1234567890",
            Bio: "Bio of John Doe.",
            DateOfBirth: new DateTime(1990, 1, 1)
        );


        // Act
        var result = _validator.Validate(userDto);

        // Assert
        Assert.False(result.IsValid, "Validation should fail for an invalid phone number.");
        Assert.Contains(result.Errors, error => error.PropertyName == "PhoneNumber" && error.ErrorMessage.Contains("Phone number must be a valid international format."));
    }
}