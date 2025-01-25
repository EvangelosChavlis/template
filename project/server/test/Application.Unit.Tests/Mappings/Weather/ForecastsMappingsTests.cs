// source
using server.src.Application.Mappings.Weather;
using server.src.Domain.Models.Weather;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Extensions;

namespace server.tests.Application.Mappings.Weather;

public class ForecastsMappingsTests
{
    [Fact]
    public void StatItemForecastDtoMapping_MapsForecastToStatItemForecastDto()
    {
        // Arrange
        var warning = new Warning { Id = Guid.NewGuid(), Name = "Heatwave" };
        var forecast = new Forecast
        {
            Id = Guid.NewGuid(),
            Date = new DateTime(2023, 12, 4, 10, 0, 0, DateTimeKind.Utc),
            TemperatureC = 35,
            Warning = warning
        };

        // Act
        var result = forecast.StatItemForecastDtoMapping();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(forecast.Id, result.Id);
        Assert.Equal(forecast.Date.GetFullLocalDateTimeString(), result.Date);
        Assert.Equal(forecast.TemperatureC, result.TemperatureC);
    }

    [Fact]
    public void ListItemForecastDtoMapping_MapsForecastToListItemForecastDto()
    {
        // Arrange
        var warning = new Warning { Id = Guid.NewGuid(), Name = "Heatwave" };
        var forecast = new Forecast
        {
            Id = Guid.NewGuid(),
            Date = new DateTime(2023, 12, 4, 10, 0, 0, DateTimeKind.Utc),
            TemperatureC = 35,
            Warning = warning
        };

        // Act
        var result = forecast.ListItemForecastDtoMapping();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(forecast.Id, result.Id);
        Assert.Equal(forecast.Date.GetFullLocalDateTimeString(), result.Date);
        Assert.Equal(forecast.TemperatureC, result.TemperatureC);
        Assert.Equal(forecast.Warning.Name, result.Warning);
    }

    [Fact]
    public void ItemForecastDtoMapping_MapsForecastToItemForecastDto()
    {
        // Arrange
        var warning = new Warning { Id = Guid.NewGuid(), Name = "Snowstorm" };
        var forecast = new Forecast
        {
            Id = Guid.NewGuid(),
            Date = new DateTime(2023, 12, 5, 15, 0, 0, DateTimeKind.Utc),
            TemperatureC = -5,
            Summary = "Heavy snow expected",
            Warning = warning
        };

        // Act
        var result = forecast.ItemForecastDtoMapping();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(forecast.Id, result.Id);
        Assert.Equal(forecast.Date.GetFullLocalDateTimeString(), result.Date);
        Assert.Equal(forecast.TemperatureC, result.TemperatureC);
        Assert.Equal(forecast.Summary, result.Summary);
        Assert.Equal(forecast.Warning.Name, result.Warning);
    }

    [Fact]
    public void CreateForecastModelMapping_CreatesForecastModelFromDto()
    {
        // Arrange
        var warning = new Warning { Id = Guid.NewGuid(), Name = "Flood Alert" };
        var forecastDto = new ForecastDto(
            Date: new DateTime(2023, 12, 6),
            TemperatureC: 22,
            Summary: "Rainy day with chances of flooding",
            WarningId: warning.Id
        );

        // Act
        var result = forecastDto.CreateForecastModelMapping(warning);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(forecastDto.Date, result.Date);
        Assert.Equal(forecastDto.TemperatureC, result.TemperatureC);
        Assert.Equal(forecastDto.Summary, result.Summary);
        Assert.Equal(warning.Id, result.WarningId);
        Assert.Equal(warning, result.Warning);
    }

    [Fact]
    public void UpdateForecastMapping_UpdatesForecastModelFromDto()
    {
        // Arrange
        var warning = new Warning { Id = Guid.NewGuid(), Name = "Storm Warning" };
        var forecastDto = new ForecastDto(
            Date: new DateTime(2023, 12, 7),
            TemperatureC: 15,
            Summary: "Windy with scattered showers",
            WarningId: warning.Id
        );
        var forecast = new Forecast
        {
            Id = Guid.NewGuid(),
            Date = new DateTime(2023, 12, 6),
            TemperatureC = 20,
            Summary = "Sunny",
            WarningId = Guid.NewGuid(),
            Warning = new Warning { Id = Guid.NewGuid(), Name = "Old Warning" }
        };

        // Act
        forecastDto.UpdateForecastMapping(forecast, warning);

        // Assert
        Assert.Equal(forecastDto.Date, forecast.Date);
        Assert.Equal(forecastDto.TemperatureC, forecast.TemperatureC);
        Assert.Equal(forecastDto.Summary, forecast.Summary);
        Assert.Equal(warning.Id, forecast.WarningId);
        Assert.Equal(warning, forecast.Warning);
    }
}