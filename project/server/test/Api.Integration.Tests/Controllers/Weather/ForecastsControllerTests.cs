// packages
using System.Net.Http.Json;
using Newtonsoft.Json;

// source
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Extensions;
using server.src.Domain.Models.Weather;

// test
using server.test.Api.Integration.Tests.TestHelpers;

namespace server.test.Api.Integration.Tests.Controllers.Weather;

public class ForecastsControllerTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly TestingWebAppFactory<Program> _factory;

    public ForecastsControllerTests(TestingWebAppFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    private void ClearAndSeedDatabase()
    {
        var context = _factory.GetDataContext();

        // Clear the database (no transaction needed)
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.Forecasts.RemoveRange(context.Forecasts); // Clear existing data
        context.SaveChanges();

        // Seed fresh data for each test
        var warning = new Warning
        {
            Name = "Thunderstorm",
            Description = "Severe thunderstorm warning"
        };
        context.Warnings.Add(warning);
        context.SaveChanges();

        context.Forecasts.Add(new Forecast
        {
            Date = DateTime.Now,
            TemperatureC = 30,
            Summary = "Sunny",
            WarningId = warning.Id
        });

        context.SaveChanges();
    }


    [Fact]
    public async Task GetForecasts_ReturnsListOfForecasts()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        // Act
        var response = await _client.GetAsync("/api/weather/forecasts");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ListResponse<List<ListItemForecastDto>>>(content);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Data!);
        Assert.Contains(result.Data!, forecast => forecast.TemperatureC == 30);
    }

    [Fact]
    public async Task GetForecastById_ReturnsSingleForecast()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        // Retrieve a forecast from the in-memory database using a query
        var forecast = context.Forecasts.FirstOrDefault(f => f.Summary == "Sunny");

        // Ensure that the forecast exists
        Assert.NotNull(forecast);
        var forecastId = forecast.Id;

        // Act
        var response = await _client.GetAsync($"/api/weather/forecasts/{forecastId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ItemResponse<ItemForecastDto>>(content);

        Assert.NotNull(result);
        Assert.Equal(forecastId, result.Data!.Id);
        Assert.Equal("Sunny", result.Data.Summary);
        Assert.Equal(30, result.Data.TemperatureC);
    }

    [Fact]
    public async Task CreateForecast_ReturnsCreatedForecast()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        var warning = context.Warnings.FirstOrDefault(w => w.Name == "Thunderstorm")!;

        var newForecast = new ForecastDto(
            Date: DateTime.Now.AddDays(1),
            TemperatureC: 35,
            Summary: "Heatwave",
            WarningId: warning.Id
        );

        // Act
        var response = await _client.PostAsJsonAsync("/api/weather/forecasts", newForecast);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CommandResponse<string>>(content);

        Assert.NotNull(result);
        Assert.Equal($"Forecast {newForecast.Date.GetLocalDateString()} inserted successfully!", result.Data);
    }

    [Fact]
    public async Task UpdateForecast_ReturnsUpdatedForecast()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        var existingForecast = context.Forecasts.FirstOrDefault(f => f.Summary == "Sunny");

        // Ensure that the forecast exists
        Assert.NotNull(existingForecast);
        var forecastId = existingForecast.Id;

        // Create the updated forecast DTO
        var updatedForecast = new ForecastDto(
            Date: DateTime.Now.AddDays(2),
            TemperatureC: 40,
            Summary: "Extreme Heat",
            WarningId: existingForecast.WarningId
        );

        // Act
        var response = await _client.PutAsJsonAsync($"/api/weather/forecasts/{forecastId}", updatedForecast);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CommandResponse<string>>(content);

        Assert.NotNull(result);
        Assert.Equal($"Forecast {updatedForecast.Date.GetLocalDateString()} updated successfully!", result.Data);
    }

    [Fact]
    public async Task DeleteForecast_ReturnsSuccess()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        var forecast = context.Forecasts.FirstOrDefault(f => f.Summary == "Sunny");

        // Ensure the forecast exists
        Assert.NotNull(forecast);
        var forecastId = forecast.Id;

        // Act
        var response = await _client.DeleteAsync($"/api/weather/forecasts/{forecastId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CommandResponse<string>>(content);

        Assert.NotNull(result);
        Assert.Equal($"Forecast {forecast.Date.GetLocalDateString()} deleted successfully!", result.Data);
    }

    [Fact]
    public async Task InitializeForecasts_ReturnsSuccess()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        var warning = context.Warnings.FirstOrDefault(w => w.Name == "Thunderstorm")!;

        var forecasts = new List<ForecastDto>
        {
            new (
                Date: DateTime.Now.AddDays(1),
                TemperatureC: 32,
                Summary: "Cloudy",
                WarningId: warning.Id
            ),
            new (
                Date: DateTime.Now.AddDays(2),
                TemperatureC: 28,
                Summary: "Rainy",
                WarningId: warning.Id
            )
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/weather/forecasts/initialize", forecasts);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CommandResponse<string>>(content);

        Assert.NotNull(result);
    }
}
