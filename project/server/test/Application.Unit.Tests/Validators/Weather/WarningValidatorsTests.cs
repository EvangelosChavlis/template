// source
using server.src.Application.Validators.Warning;
using server.src.Domain.Dto.Weather;

namespace server.test.Application.Unit.Tests.Validators.Weather;

public class WarningValidatorsTests
{
    private readonly WarningValidators _validator;

    public WarningValidatorsTests()
    {
        _validator = new WarningValidators();
    }

    [Fact]
    public void Should_Pass_Validation_When_All_Fields_Are_Valid()
    {
        // Arrange
        var dto = new WarningDto(
            Name: "Storm Warning",
            Description: "Severe weather conditions expected."
        );

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Should_Fail_Validation_When_Name_Is_Null()
    {
        // Arrange
        var dto = new WarningDto(
            Name: null!,
            Description: "Severe weather conditions expected."
        );

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage == "Name is required.");
    }

    [Fact]
    public void Should_Fail_Validation_When_Name_Is_Empty()
    {
        // Arrange
        var dto = new WarningDto(
            Name: "",
            Description: "Severe weather conditions expected."
        );

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage == "Name is required.");
    }

    [Fact]
    public void Should_Fail_Validation_When_Name_Exceeds_Maximum_Length()
    {
        // Arrange
        var longName = new string('a', 101); // 101 characters
        var dto = new WarningDto(
            Name: longName,
            Description: "Severe weather conditions expected."
        );

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage.Contains("Name must not exceed 100 characters."));
    }

    [Fact]
    public void Should_Fail_Validation_When_Description_Is_Null()
    {
        // Arrange
        var dto = new WarningDto(
            Name: "Storm Warning",
            Description: null!
        );

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Description" && e.ErrorMessage == "Description is required.");
    }

    [Fact]
    public void Should_Fail_Validation_When_Description_Is_Empty()
    {
        // Arrange
        var dto = new WarningDto(
            Name: "Storm Warning",
            Description: ""
        );

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Description" && e.ErrorMessage == "Description is required.");
    }

    [Fact]
    public void Should_Fail_Validation_When_Description_Exceeds_Maximum_Length()
    {
        // Arrange
        var longDescription = new string('a', 501);

        var dto = new WarningDto(
            Name: "Storm Warning",
            Description: longDescription
        );

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Description" && e.ErrorMessage.Contains("Description must not exceed 500 characters."));
    }
}