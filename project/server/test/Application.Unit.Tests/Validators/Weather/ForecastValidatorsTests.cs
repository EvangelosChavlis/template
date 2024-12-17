// source
using server.src.Application.Validators.Forecast;
using server.src.Domain.Dto.Weather;

namespace server.test.Application.Unit.Tests.Validators.Weather;

public class ForecastValidatorsTests
{
    private readonly ForecastValidators _validator;

    public ForecastValidatorsTests()
    {
        _validator = new ForecastValidators();
    }

    [Fact]
    public void Should_Pass_Validation_When_All_Fields_Are_Valid()
    {
        // Arrange
        var dto = new ForecastDto(
            Date: DateTime.UtcNow,
            TemperatureC: 20,
            Summary: "Sunny day",
            WarningId: Guid.NewGuid()
        );

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Should_Fail_Validation_When_Date_Is_Null()
    {
        // Arrange
        var dto = new ForecastDto(
            Date: DateTime.MinValue,
            TemperatureC: 20,
            Summary: "Sunny day",
            WarningId: Guid.NewGuid()
        );

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Date" && e.ErrorMessage == "Date must be a valid date.");
    }

    [Fact]
    public void Should_Fail_Validation_When_Temperature_Is_Out_Of_Range()
    {
        // Arrange
        var dto = new ForecastDto(
            Date: DateTime.UtcNow,
            TemperatureC: -60,
            Summary: "Cold day",
            WarningId: Guid.NewGuid()
        );

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "TemperatureC" && e.ErrorMessage.Contains("TemperatureC must be between -50 and 50 degrees."));
    }

    [Fact]
    public void Should_Fail_Validation_When_Summary_Is_Empty()
    {
        // Arrange
        var dto = new ForecastDto(
            Date: DateTime.UtcNow,
            TemperatureC: 20,
            Summary: "",
            WarningId: Guid.NewGuid()
        );

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Summary" && e.ErrorMessage.Contains("Summary is required."));
    }

    [Fact]
    public void Should_Fail_Validation_When_WarningId_Is_Invalid()
    {
        // Arrange
        var dto = new ForecastDto(
            Date: DateTime.UtcNow,
            TemperatureC: 20,
            Summary: "Sunny day",
            WarningId: Guid.Empty
        );

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "WarningId" && e.ErrorMessage.Contains("WarningId must be a valid GUID."));
    }

    [Fact]
    public void Should_Fail_Validation_When_Summary_Exceeds_Maximum_Length()
    {
        // Arrange
        var longSummary = new string('a', 201); // 201 characters
        var dto = new ForecastDto(
            Date: DateTime.UtcNow,
            TemperatureC: 20,
            Summary: longSummary,
            WarningId: Guid.NewGuid()
        );

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Summary" && e.ErrorMessage.Contains("Summary must not exceed 200 characters."));
    }
}
