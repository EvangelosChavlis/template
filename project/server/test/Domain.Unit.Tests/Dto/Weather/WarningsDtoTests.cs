// source
using server.src.Domain.Dto.Weather;

namespace server.test.Domain.Unit.Tests.Dto.Weather;

public class WarningsDtoTests
{
    [Fact]
    public void Should_Create_Valid_ListItemWarningDto()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "Storm Warning";
        var description = "Severe storm expected in the area.";
        var count = 5;

        // Act
        var dto = new ListItemWarningDto(id, name, description, count);

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(name, dto.Name);
        Assert.Equal(description, dto.Description);
        Assert.Equal(count, dto.Count);
    }


    [Fact]
    public void Should_Create_Valid_PickerWarningDto()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "Flood Warning";

        // Act
        var dto = new PickerWarningDto(id, name);

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(name, dto.Name);
    }


    [Fact]
    public void Should_Create_Valid_ItemWarningDto()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "Heat Wave Warning";
        var description = "Temperatures exceeding 40°C.";
        var forecasts = new List<string> { "Day 1: 42°C", "Day 2: 43°C" };

        // Act
        var dto = new ItemWarningDto(id, name, description, forecasts);

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(name, dto.Name);
        Assert.Equal(description, dto.Description);
        Assert.Equal(forecasts, dto.Forecasts);
    }

    [Fact]
    public void Should_Create_Valid_WarningDto()
    {
        // Arrange
        var name = "Flood Warning";
        var description = "Heavy rainfall expected.";

        // Act
        var dto = new WarningDto(name, description);

        // Assert
        Assert.Equal(name, dto.Name);
        Assert.Equal(description, dto.Description);
    }
}