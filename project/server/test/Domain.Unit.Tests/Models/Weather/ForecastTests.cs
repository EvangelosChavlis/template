// source
using server.src.Domain.Models.Weather;

namespace server.test.Domain.Models.Weather;

public class ForecastTests
{
    [Fact]
    public void Forecast_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var forecast = new Forecast();

        // Assert
        Assert.Equal(Guid.Empty, forecast.Id);  // Default value for Guid
        Assert.Equal(default, forecast.Date);  // Default value for DateTime
        Assert.Equal(0, forecast.TemperatureC);  // Default value for integer
        Assert.Null(forecast.Summary);  // Default value for string
        Assert.Equal(Guid.Empty, forecast.WarningId);  // Default value for Guid
        Assert.Null(forecast.Warning);  // Default value for navigation property
    }

    [Fact]
    public void Forecast_SetProperties_ShouldAssignValuesCorrectly()
    {
        // Arrange
        var forecast = new Forecast
        {
            Id = Guid.NewGuid(),
            Date = new DateTime(2024, 12, 5),
            TemperatureC = 25,
            Summary = "Sunny",
            WarningId = Guid.NewGuid(),
            Warning = new Warning { Id = Guid.NewGuid(), Name = "Heatwave", Description = "Extreme heat expected" }
        };

        // Act & Assert
        Assert.NotEqual(Guid.Empty, forecast.Id);  // Ensure Id is set
        Assert.Equal(new DateTime(2024, 12, 5), forecast.Date);  // Ensure Date is set
        Assert.Equal(25, forecast.TemperatureC);  // Ensure TemperatureC is set
        Assert.Equal("Sunny", forecast.Summary);  // Ensure Summary is set
        Assert.NotEqual(Guid.Empty, forecast.WarningId);  // Ensure WarningId is set
        Assert.NotNull(forecast.Warning);  // Ensure Warning navigation property is set
        Assert.Equal("Heatwave", forecast.Warning.Name);  // Ensure Warning object is set correctly
    }

    [Fact]
    public void Forecast_EmptyConstructor_ShouldHaveDefaultValues()
    {
        // Arrange & Act
        var forecast = new Forecast();

        // Assert
        Assert.Equal(Guid.Empty, forecast.Id);  // Default value for Id
        Assert.Equal(default, forecast.Date);  // Default value for Date
        Assert.Equal(0, forecast.TemperatureC);  // Default value for TemperatureC
        Assert.Null(forecast.Summary);  // Default value for Summary
        Assert.Equal(Guid.Empty, forecast.WarningId);  // Default value for WarningId
        Assert.Null(forecast.Warning);  // Default value for Warning navigation property
    }
}