// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

// source
using server.src.Domain.Models.Auth;
using server.src.Persistence.Configurations.Auth;
using server.src.Persistence.Contexts;

namespace server.test.Persistence.Configurations.Auth;

public class RoleConfigurationTests
{
    private readonly string _dbName = "RoleTestDatabase";
    private DbContextOptions<DataContext> GetInMemoryDbContextOptions()
    {
        // Return in-memory database options for testing purposes
        return new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(_dbName) // In-memory database for testing
            .Options;
    }

    private ModelBuilder GetModelBuilder()
    {
        var options = GetInMemoryDbContextOptions();
        var context = new DataContext(options);
        return new ModelBuilder(new ConventionSet());
    }

    [Fact]
    public void Configure_ShouldSetPrimaryKeyForRole()
    {
        // Arrange
        var modelBuilder = GetModelBuilder();
        var configuration = new RoleConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Role>());

        // Assert
        var entity = modelBuilder.Model.FindEntityType(typeof(Role));
        var primaryKey = entity!.FindPrimaryKey();

        // Check that the primary key is set on the "Id" property
        Assert.NotNull(primaryKey);
        Assert.Contains(primaryKey.Properties, p => p.Name == "Id");
    }

    [Fact]
    public void Configure_ShouldSetRelationshipWithUserRoles()
    {
        // Arrange
        var modelBuilder = GetModelBuilder();
        var configuration = new RoleConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<Role>());

        // Assert
        var entity = modelBuilder.Model.FindEntityType(typeof(Role));
        var navigation = entity!.FindNavigation("UserRoles");

        // Check that the relationship is correctly configured
        Assert.NotNull(navigation);

        // Compare the full name (including the namespace) of the related entity type
        Assert.Equal("server.src.Domain.Models.Auth.Role", navigation.ForeignKey.PrincipalEntityType.Name); // Full name with namespace
        Assert.Equal("RoleId", navigation.ForeignKey.Properties[0].Name); // ForeignKey property
        Assert.True(navigation.ForeignKey.IsRequired); // Ensure the relationship is required (IsRequired)
    }
}