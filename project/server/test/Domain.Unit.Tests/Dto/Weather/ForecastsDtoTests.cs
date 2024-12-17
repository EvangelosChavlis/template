//source
using server.src.Domain.Dto.Weather;

namespace server.test.Domain.Unit.Tests.Dto.Weather;

public class ForecastsDtoTests
{
    [Fact]
    public void Should_Create_Valid_ListItemForecastDto()
    {
        // Arrange
        var id = Guid.NewGuid();
        var date = "2024-12-15";
        var temperatureC = 25;
        var warning = "Storm Alert";

        // Act
        var dto = new ListItemForecastDto(id, date, temperatureC, warning);

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(date, dto.Date);
        Assert.Equal(temperatureC, dto.TemperatureC);
        Assert.Equal(warning, dto.Warning);
    }

    [Fact]
    public void Should_Create_Valid_ItemForecastDto()
    {
        // Arrange
        var id = Guid.NewGuid();
        var date = "2024-12-15";
        var temperatureC = 15;
        var summary = "Cloudy";
        var warning = "Rain Alert";

        // Act
        var dto = new ItemForecastDto(id, date, temperatureC, summary, warning);

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(date, dto.Date);
        Assert.Equal(temperatureC, dto.TemperatureC);
        Assert.Equal(summary, dto.Summary);
        Assert.Equal(warning, dto.Warning);
    }

    [Fact]
    public void Should_Create_Valid_ForecastDto()
    {
        // Arrange
        var date = DateTime.UtcNow;
        var temperatureC = 20;
        var summary = "Clear skies";
        var warningId = Guid.NewGuid();

        // Act
        var dto = new ForecastDto(date, temperatureC, summary, warningId);

        // Assert
        Assert.Equal(date, dto.Date);
        Assert.Equal(temperatureC, dto.TemperatureC);
        Assert.Equal(summary, dto.Summary);
        Assert.Equal(warningId, dto.WarningId);
    }
}