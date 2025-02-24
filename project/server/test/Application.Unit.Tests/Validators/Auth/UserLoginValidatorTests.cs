// // packages
// using FluentValidation.TestHelper;

// // source
// using server.src.Application.Validators.Auth;
// using server.src.Domain.Dto.Auth;

// namespace server.test.Application.Unit.Tests.Validators.Auth;

// public class UserLoginValidatorTests
// {
//     private readonly UserLoginValidators _validator;

//     public UserLoginValidatorTests()
//     {
//         _validator = new UserLoginValidators();
//     }

//     // Username tests
//     [Fact]
//     public void Should_Have_Error_When_Username_Is_Null()
//     {
//         // Arrange
//         var dto = new UserLoginDto(Username: null!, Password: "ValidPassword123");

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldHaveValidationErrorFor(u => u.Username)
//             .WithErrorMessage("Username is required.");
//     }

//     [Fact]
//     public void Should_Have_Error_When_Username_Is_Empty()
//     {
//         // Arrange
//         var dto = new UserLoginDto(Username: string.Empty, Password: "ValidPassword123");

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldHaveValidationErrorFor(u => u.Username)
//             .WithErrorMessage("Username is required.");
//     }

//     [Fact]
//     public void Should_Not_Have_Error_When_Username_Is_Valid()
//     {
//         // Arrange
//         var dto = new UserLoginDto(Username: "validusername", Password: "ValidPassword123");

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldNotHaveValidationErrorFor(u => u.Username);
//     }

//     // Password tests
//     [Fact]
//     public void Should_Have_Error_When_Password_Is_Null()
//     {
//         // Arrange
//         var dto = new UserLoginDto(Username: "validusername", Password: null!);

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldHaveValidationErrorFor(u => u.Password)
//             .WithErrorMessage("Password is required.");
//     }

//     [Fact]
//     public void Should_Have_Error_When_Password_Is_Empty()
//     {
//         // Arrange
//         var dto = new UserLoginDto(Username: "validusername", Password: string.Empty);

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldHaveValidationErrorFor(u => u.Password)
//             .WithErrorMessage("Password is required.");
//     }

//     [Fact]
//     public void Should_Not_Have_Error_When_Password_Is_Valid()
//     {
//         // Arrange
//         var dto = new UserLoginDto(Username: "validusername", Password: "ValidPassword123");

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldNotHaveValidationErrorFor(u => u.Password);
//     }
// }