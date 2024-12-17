// source
using server.src.Domain.Models.Weather;
using server.src.Application.Filters.Weather;

namespace server.test.Application.Unit.Tests.Filters.Weather;

public class ForecastFiltersTests
{
    [Fact]
    public void ForecastTempSorting_ShouldReturnCorrectPropertyName()
    {
        // Act
        var result = ForecastFilters.ForecastTempSorting;

        // Assert
        Assert.Equal(nameof(Forecast.TemperatureC), result);
    }

    [Theory]
    [InlineData("2024", true, true)]
    [InlineData("Sunny", true, false)]
    [InlineData("200", false, false)]
    [InlineData("22", true, false)]
    [InlineData("", true, true)]
    [InlineData("Rainy", false, false)]
    public void ForecastSearchFilter_ShouldFilterForecastsBasedOnInput(
        string filter,
        bool matchesFirstForecast,
        bool matchesSecondForecast)
    {
        // Arrange
        var forecastData = new List<Forecast>
        {
            new() {
                Id = Guid.NewGuid(),
                Date = new DateTime(2024, 12, 7, 15, 30, 0),
                TemperatureC = 22,
                Summary = "Sunny"
            },
            new() {
                Id = Guid.NewGuid(),
                Date = new DateTime(2024, 12, 8, 9, 0, 0),
                TemperatureC = 18,
                Summary = "Cloudy"
            }
        };

        var searchFilter = filter.ForecastSearchFilter();

        // Act
        var filteredForecasts = forecastData.AsQueryable().Where(searchFilter).ToList();

        // Assert
        Assert.Equal(matchesFirstForecast, filteredForecasts.Any(f => f.Summary == "Sunny"));
        Assert.Equal(matchesSecondForecast, filteredForecasts.Any(f => f.Summary == "Cloudy"));
    }
}