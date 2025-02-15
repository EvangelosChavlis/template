// // packages
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Conventions;

// // source
// using server.src.Domain.Models.Weather;
// using server.src.Persistence.Configurations.Weather;
// using server.src.Persistence.Contexts;

// namespace server.test.Persistence.Configurations.Weather;

// public class ForecastConfigurationTests
// {
//     private readonly string _dbName = "WeatherTestDatabase";

//     private DbContextOptions<DataContext> GetInMemoryDbContextOptions()
//     {
//         // Return in-memory database options for testing purposes
//         return new DbContextOptionsBuilder<DataContext>()
//             .UseInMemoryDatabase(_dbName) // Ensure the package is installed and using the correct namespace
//             .Options;
//     }

//     private ModelBuilder GetModelBuilder()
//     {
//         var options = GetInMemoryDbContextOptions();
//         var context = new DataContext(options);
//         return new ModelBuilder(new ConventionSet());
//     }

//     [Fact]
//     public void Configure_ShouldSetPrimaryKeyForForecast()
//     {
//         // Arrange
//         var modelBuilder = GetModelBuilder();
//         var configuration = new ForecastConfiguration();

//         // Act
//         configuration.Configure(modelBuilder.Entity<Forecast>());

//         // Assert
//         var entity = modelBuilder.Model.FindEntityType(typeof(Forecast));
//         var primaryKey = entity!.FindPrimaryKey();

//         // Check that the primary key is set on the "Id" property
//         Assert.NotNull(primaryKey);
//         Assert.Contains(primaryKey.Properties, p => p.Name == "Id");
//     }

//     [Fact]
//     public void Configure_ShouldSetDateAndTemperatureCAsRequired()
//     {
//         // Arrange
//         var modelBuilder = GetModelBuilder();
//         var configuration = new ForecastConfiguration();

//         // Act
//         configuration.Configure(modelBuilder.Entity<Forecast>());

//         // Assert
//         var entity = modelBuilder.Model.FindEntityType(typeof(Forecast));

//         var dateProperty = entity!.FindProperty("Date");
//         Assert.NotNull(dateProperty);
//         Assert.False(dateProperty.IsNullable);

//         var temperatureCProperty = entity.FindProperty("TemperatureC");
//         Assert.NotNull(temperatureCProperty);
//         Assert.False(temperatureCProperty.IsNullable);
//     }

//     [Fact]
//     public void Configure_ShouldSetSummaryMaxLength()
//     {
//         // Arrange
//         var modelBuilder = GetModelBuilder();
//         var configuration = new ForecastConfiguration();

//         // Act
//         configuration.Configure(modelBuilder.Entity<Forecast>());

//         // Assert
//         var entity = modelBuilder.Model.FindEntityType(typeof(Forecast));
//         var summaryProperty = entity!.FindProperty("Summary");

//         Assert.NotNull(summaryProperty);
//         Assert.Equal(200, summaryProperty.GetMaxLength());
//     }

//     [Fact]
//     public void Configure_ShouldSetRelationshipWithWarning()
//     {
//         // Arrange
//         var modelBuilder = GetModelBuilder();
//         var configuration = new ForecastConfiguration();

//         // Act
//         configuration.Configure(modelBuilder.Entity<Forecast>());

//         // Assert
//         var entity = modelBuilder.Model.FindEntityType(typeof(Forecast));
//         var navigation = entity!.FindNavigation("Warning");

//         Assert.NotNull(navigation);
        
//         // Compare the full name (including the namespace) of the related entity type
//         Assert.Equal("server.src.Domain.Models.Weather.Warning", navigation.ForeignKey.PrincipalEntityType.Name);
//         Assert.Equal(DeleteBehavior.Cascade, navigation.ForeignKey.DeleteBehavior);
//     }
// }
