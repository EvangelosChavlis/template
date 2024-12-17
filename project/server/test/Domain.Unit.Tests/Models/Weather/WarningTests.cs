// source
using server.src.Domain.Models.Weather;

namespace server.test.Domain.Unit.Tests.Models.Weather;

public class WarningTests
{
    [Fact]
    public void Warning_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var warning = new Warning
            {
                // Explicitly initialize the Forecasts list
                Forecasts = []
            };


        // Assert
        Assert.Equal(Guid.Empty, warning.Id);  // Default value for Guid
        Assert.Null(warning.Name);  // Default value for string
        Assert.Null(warning.Description);  // Default value for string
        Assert.NotNull(warning.Forecasts);  // Should initialize the list to an empty list
        Assert.Empty(warning.Forecasts);  // Initially, the list should be empty
    }

    [Fact]
    public void Warning_SetProperties_ShouldAssignValuesCorrectly()
    {
        // Arrange
        var warning = new Warning
        {
            Id = Guid.NewGuid(),
            Name = "Flood Warning",
            Description = "Severe flooding in the area",
            Forecasts =
            [
                new() { Id = Guid.NewGuid(), Date = DateTime.Now, TemperatureC = 15, Summary = "Rainy" }
            ]
        };

        // Act & Assert
        Assert.NotEqual(Guid.Empty, warning.Id);  // Ensure Id is set
        Assert.Equal("Flood Warning", warning.Name);  // Ensure Name is set
        Assert.Equal("Severe flooding in the area", warning.Description);  // Ensure Description is set
        Assert.NotEmpty(warning.Forecasts);  // Ensure Forecasts list is populated
        Assert.Single(warning.Forecasts);  // Only one forecast in the list
        Assert.Equal(15, warning.Forecasts[0].TemperatureC);  // Check the temperature of the first forecast
    }

    [Fact]
    public void Warning_Forecasts_ShouldLinkToForecastCorrectly()
    {
        // Arrange
        var warning = new Warning
        {
            Id = Guid.NewGuid(),
            Name = "Storm Warning",
            Description = "Severe storm is approaching",
            Forecasts =
            [
                new() 
                { 
                    Id = Guid.NewGuid(), 
                    Date = DateTime.Now, 
                    TemperatureC = 12, 
                    Summary = "Windy" 
                },
                new() 
                { 
                    Id = Guid.NewGuid(), 
                    Date = DateTime.Now.AddDays(1), 
                    TemperatureC = 10, 
                    Summary = "Snowy" 
                }
            ]
        };

        // Act & Assert
        Assert.Equal(2, warning.Forecasts.Count);  // There should be two forecasts
        Assert.Equal(12, warning.Forecasts[0].TemperatureC);  // First forecast should have temperature 12
        Assert.Equal(10, warning.Forecasts[1].TemperatureC);  // Second forecast should have temperature 10
        Assert.Equal("Storm Warning", warning.Name);  // Ensure the warning's Name is correct
    }
}