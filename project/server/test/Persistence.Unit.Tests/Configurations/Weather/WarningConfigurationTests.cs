// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

// source
using server.src.Domain.Models.Weather;
using server.src.Persistence.Configurations.Weather;
using server.src.Persistence.Contexts;

namespace server.test.Persistence.Configurations.Weather;

public class WarningConfigurationTests
{
    private readonly string _dbName = "WeatherTestDatabase";

    private DbContextOptions<DataContext> GetInMemoryDbContextOptions()
    {
        // Return in-memory database options for testing purposes
        return new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(_dbName)
            .Options;
    }

    private ModelBuilder GetModelBuilder()
    {
        var options = GetInMemoryDbContextOptions();
        var context = new DataContext(options);
        return new ModelBuilder(new ConventionSet());
    }

    [Fact]
    public void Configure_ShouldSetPrimaryKeyForWarning()
    {
        // Arrange
        var modelBuilder = GetModelBuilder();
        var configuration = new WarningConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Warning>());

        // Assert
        var entity = modelBuilder.Model.FindEntityType(typeof(Warning));
        var primaryKey = entity!.FindPrimaryKey();

        // Check that the primary key is set on the "Id" propertya
        Assert.NotNull(primaryKey);
        Assert.Contains(primaryKey.Properties, p => p.Name == "Id");
    }

    [Fact]
    public void Configure_ShouldSetNameAsRequiredWithMaxLength()
    {
        // Arrange
        var modelBuilder = GetModelBuilder();
        var configuration = new WarningConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Warning>());

        // Assert
        var entity = modelBuilder.Model.FindEntityType(typeof(Warning));
        var nameProperty = entity!.FindProperty("Name");

        Assert.NotNull(nameProperty);
        Assert.False(nameProperty.IsNullable);
        Assert.Equal(100, nameProperty.GetMaxLength());
    }

    [Fact]
    public void Configure_ShouldSetDescriptionMaxLength()
    {
        // Arrange
        var modelBuilder = GetModelBuilder();
        var configuration = new WarningConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Warning>());

        // Assert
        var entity = modelBuilder.Model.FindEntityType(typeof(Warning));
        var descriptionProperty = entity!.FindProperty("Description");

        Assert.NotNull(descriptionProperty);
        Assert.Equal(500, descriptionProperty.GetMaxLength());
    }

[Fact]
public void Configure_ShouldSetRelationshipWithForecasts()
{
    // Arrange
    var modelBuilder = GetModelBuilder();
    var configuration = new WarningConfiguration();

    // Act
    configuration.Configure(modelBuilder.Entity<Warning>());

    // Assert
    var entity = modelBuilder.Model.FindEntityType(typeof(Warning));
    var navigation = entity!.FindNavigation("Forecasts"); // Ensure checking the plural "Forecasts"

    Assert.NotNull(navigation); // Ensure navigation is not null
    Assert.Equal("Forecast", navigation.TargetEntityType.ClrType.Name);  // Assert the name of the target entity (dependent entity), which should be "Forecast"
    Assert.Equal(DeleteBehavior.Cascade, navigation.ForeignKey.DeleteBehavior);  // Assert delete behavior
}

}