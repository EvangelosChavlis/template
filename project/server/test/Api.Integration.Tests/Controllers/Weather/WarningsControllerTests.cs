// packages
using System.Net.Http.Json;
using Newtonsoft.Json;

// source
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Models.Weather;

// packages
using server.test.Api.Integration.Tests.TestHelpers;

namespace server.test.Api.Integration.Tests.Controllers.Weather;

public class WarningsControllerTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly TestingWebAppFactory<Program> _factory;

    public WarningsControllerTests(TestingWebAppFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    private void ClearAndSeedDatabase()
    {
        var context = _factory.GetDataContext();
        context.Database.EnsureCreated();
        context.Warnings.RemoveRange(context.Warnings); // Clear existing data
        context.SaveChanges();

        // Seed fresh data for each test
        context.Warnings.Add(new Warning
        {
            Name = "Thunderstorm",
            Description = "Severe thunderstorm warning",
        });

        context.Warnings.Add(new Warning
        {
            Name = "Flood",
            Description = "Flood warning"
        });

        context.SaveChanges();
    }

    [Fact]
    public async Task GetWarnings_ReturnsListOfWarnings()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        // Act
        var response = await _client.GetAsync("/api/weather/warnings");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ListResponse<List<ListItemWarningDto>>>(content);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Data!);
        Assert.Contains(result.Data!, warning => warning.Name == "Thunderstorm");
        Assert.Contains(result.Data!, warning => warning.Name == "Flood");
    }

    [Fact]
    public async Task GetWarningById_ReturnsSingleWarning()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        // Retrieve a warning from the in-memory database using a query
        var warning = context.Warnings.FirstOrDefault(w => w.Name == "Thunderstorm");

        // Ensure that the warning exists
        Assert.NotNull(warning);
        var warningId = warning.Id;

        // Act
        var response = await _client.GetAsync($"/api/weather/warnings/{warningId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ItemResponse<ItemWarningDto>>(content);

        Assert.NotNull(result);
        Assert.Equal(warningId, result.Data!.Id);
        Assert.Equal("Thunderstorm", result.Data.Name);
        Assert.Equal("Severe thunderstorm warning", result.Data.Description);
    }

    [Fact]
    public async Task GetWarningsPicker_ReturnsListOfPickerWarnings()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        // Act
        var response = await _client.GetAsync("/api/weather/warnings/picker");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CommandResponse<List<PickerWarningDto>>>(content);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Data!);
        Assert.Contains(result.Data!, warning => warning.Name == "Thunderstorm");
    }

    [Fact]
    public async Task InitializeWarning_ReturnsSuccess()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        var warnings = new List<WarningDto>
        {
            new (Name: "Thunderstorm", Description: "Severe thunderstorm warning"),
            new (Name: "Flood", Description: "Flood warning")
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/weather/warnings/initialize", warnings);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CommandResponse<string>>(content);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task CreateWarning_ReturnsCreatedWarning()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        var newWarning = new WarningDto("Heatwave", "Extreme heat warning");

        // Act
        var response = await _client.PostAsJsonAsync("/api/weather/warnings", newWarning);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CommandResponse<string>>(content);

        Assert.NotNull(result);
        Assert.Equal($"Warning {newWarning.Name} inserted successfully!", result.Data);
    }

    [Fact]
    public async Task UpdateWarning_ReturnsUpdatedWarning()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        // Retrieve a valid warning from the in-memory database
        var existingWarning = context.Warnings.FirstOrDefault(w => w.Name == "Thunderstorm");

        // Ensure that the warning exists
        Assert.NotNull(existingWarning);
        var warningId = existingWarning.Id;

        // Create the updated warning DTO
        var updatedWarning = new WarningDto(Name: "Updated Heatwave", Description: "Updated extreme heat warning");

        // Act
        var response = await _client.PutAsJsonAsync($"/api/weather/warnings/{warningId}", updatedWarning);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CommandResponse<string>>(content);

        Assert.NotNull(result);
        Assert.Equal($"Warning {updatedWarning.Name} updated successfully!", result.Data);
    }

    [Fact]
    public async Task DeleteWarning_ReturnsSuccess()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        var warning = context.Warnings.FirstOrDefault(w => w.Name == "Thunderstorm");

        // Ensure the warning exists
        Assert.NotNull(warning);
        var warningId = warning.Id;

        // Act
        var response = await _client.DeleteAsync($"/api/weather/warnings/{warningId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CommandResponse<string>>(content);

        Assert.NotNull(result);
        Assert.Equal($"Warning {warning.Name} deleted successfully!", result.Data);
    }
}
