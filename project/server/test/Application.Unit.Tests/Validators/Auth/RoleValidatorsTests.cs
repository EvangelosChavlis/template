// // source
// using server.src.Application.Validators.Role;
// using server.src.Domain.Dto.Auth;

// namespace server.test.Application.Unit.Tests.Validators.Auth;

// public class RoleValidatorsTests
// {
//     private readonly RoleValidators _validator;

//     public RoleValidatorsTests()
//     {
//         _validator = new RoleValidators();
//     }

//     [Fact]
//     public void Should_PassValidation_When_ValidInput()
//     {
//         // Arrange
//         var roleDto = new RoleDto(
//             Name: "Administrator", 
//             Description: "Manages all system resources"
//         );

//         // Act
//         var result = _validator.Validate(roleDto);

//         // Assert
//         Assert.True(result.IsValid, "Validation should pass for valid input.");
//     }

//     [Fact]
//     public void Should_FailValidation_When_NameIsNull()
//     {
//         // Arrange
//         var roleDto = new RoleDto(
//             Name: null!, 
//             Description: "Manages all system resources"
//         );

//         // Act
//         var result = _validator.Validate(roleDto);

//         // Assert
//         Assert.False(result.IsValid, "Validation should fail when Name is null.");
//         Assert.Contains(result.Errors, error => error.PropertyName == "Name" && error.ErrorMessage == "Name is required.");
//     }

//     [Fact]
//     public void Should_FailValidation_When_NameIsEmpty()
//     {
//         // Arrange
//         var roleDto = new RoleDto(
//             Name: "", 
//             Description: "Manages all system resources"
//         );

//         // Act
//         var result = _validator.Validate(roleDto);

//         // Assert
//         Assert.False(result.IsValid, "Validation should fail when Name is empty.");
//         Assert.Contains(result.Errors, error => error.PropertyName == "Name" && error.ErrorMessage == "Name is required.");
//     }

//     [Fact]
//     public void Should_FailValidation_When_NameExceedsMaxLength()
//     {
//         // Arrange
//         var longName = new string('A', 101); // 101 characters
//         var roleDto = new RoleDto(
//             Name: longName, 
//             Description: "Manages all system resources"
//         );

//         // Act
//         var result = _validator.Validate(roleDto);

//         // Assert
//         Assert.False(result.IsValid, "Validation should fail when Name exceeds 100 characters.");
//         Assert.Contains(result.Errors, error => error.PropertyName == "Name" && error.ErrorMessage == "Name must not exceed 100 characters.");
//     }

//     [Fact]
//     public void Should_FailValidation_When_DescriptionIsNull()
//     {
//         // Arrange
//         var roleDto = new RoleDto(
//             Name: "Administrator", 
//             Description: null!
//         );

//         // Act
//         var result = _validator.Validate(roleDto);

//         // Assert
//         Assert.False(result.IsValid, "Validation should fail when Description is null.");
//         Assert.Contains(result.Errors, error => error.PropertyName == "Description" && error.ErrorMessage == "Description is required.");
//     }

//     [Fact]
//     public void Should_FailValidation_When_DescriptionIsEmpty()
//     {
//         // Arrange
//         var roleDto = new RoleDto(
//             Name: "Administrator", 
//             Description: ""
//         );

//         // Act
//         var result = _validator.Validate(roleDto);

//         // Assert
//         Assert.False(result.IsValid, "Validation should fail when Description is empty.");
//         Assert.Contains(result.Errors, error => error.PropertyName == "Description" && error.ErrorMessage == "Description is required.");
//     }

//     [Fact]
//     public void Should_FailValidation_When_DescriptionExceedsMaxLength()
//     {
//         // Arrange
//         var longDescription = new string('D', 251); // 251 characters
//         var roleDto = new RoleDto(
//             Name: "Administrator", 
//             Description: longDescription
//         );

//         // Act
//         var result = _validator.Validate(roleDto);

//         // Assert
//         Assert.False(result.IsValid, "Validation should fail when Description exceeds 250 characters.");
//         Assert.Contains(result.Errors, error => error.PropertyName == "Description" && error.ErrorMessage == "Description must not exceed 250 characters.");
//     }
// }