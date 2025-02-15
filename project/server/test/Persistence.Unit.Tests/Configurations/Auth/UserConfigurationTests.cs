// // packages
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Conventions;

// // source
// using server.src.Domain.Models.Auth;
// using server.src.Persistence.Configurations.Auth;
// using server.src.Persistence.Contexts;

// namespace server.test.Persistence.Configurations.Auth;

// public class UserConfigurationTests
// {
//     private readonly string _dbName = "UserTestDatabase";
    
//     private DbContextOptions<DataContext> GetInMemoryDbContextOptions()
//     {
//         // Return in-memory database options for testing purposes
//         return new DbContextOptionsBuilder<DataContext>()
//             .UseInMemoryDatabase(_dbName) // In-memory database for testing
//             .Options;
//     }

//     private ModelBuilder GetModelBuilder()
//     {
//         var options = GetInMemoryDbContextOptions();
//         var context = new DataContext(options);
//         return new ModelBuilder(new ConventionSet());
//     }

//     [Fact]
//     public void Configure_ShouldSetPrimaryKeyForUser()
//     {
//         // Arrange
//         var modelBuilder = GetModelBuilder();
//         var configuration = new UserConfiguration();

//         // Act
//         configuration.Configure(modelBuilder.Entity<User>());

//         // Assert
//         var entity = modelBuilder.Model.FindEntityType(typeof(User));
//         var primaryKey = entity!.FindPrimaryKey();

//         // Check that the primary key is set on the "Id" property
//         Assert.NotNull(primaryKey);
//         Assert.Contains(primaryKey.Properties, p => p.Name == "Id");
//     }

//     [Fact]
//     public void Configure_ShouldSetRelationshipWithUserRoles()
//     {
//         // Arrange
//         var modelBuilder = GetModelBuilder();
//         var configuration = new UserConfiguration();

//         // Act
//         configuration.Configure(modelBuilder.Entity<User>());

//         // Assert
//         var entity = modelBuilder.Model.FindEntityType(typeof(User));
//         var navigation = entity!.FindNavigation("UserRoles");

//         // Check that the relationship is correctly configured
//         Assert.NotNull(navigation);

//         // Use the full name of the PrincipalEntityType for comparison
//         var expectedTypeName = typeof(User).FullName; // Get the full name (namespace + class name)

//         Assert.Equal(expectedTypeName, navigation.ForeignKey.PrincipalEntityType.Name); // Ensure relationship to User
//         Assert.Equal("UserId", navigation.ForeignKey.Properties[0].Name); // ForeignKey property
//         Assert.True(navigation.ForeignKey.IsRequired); // Ensure the relationship is required (IsRequired)
//     }
// }