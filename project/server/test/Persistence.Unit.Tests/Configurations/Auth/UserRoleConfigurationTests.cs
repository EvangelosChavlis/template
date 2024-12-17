using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

using server.src.Domain.Models.Auth;
using server.src.Persistence.Configurations.Auth;
using server.src.Persistence.Contexts;

namespace server.test.Persistence.Configurations.Auth;

public class UserRoleConfigurationTests
{
    private readonly string _dbName = "UserRoleTestDatabase";
    
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
    public void Configure_ShouldSetPrimaryKeyForUserRole()
    {
        // Arrange
        var modelBuilder = GetModelBuilder();
        var configuration = new UserRoleConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<UserRole>());

        // Assert
        var entity = modelBuilder.Model.FindEntityType(typeof(UserRole));
        var primaryKey = entity!.FindPrimaryKey();

        // Check that the primary key is set on the "UserId" and "RoleId" properties
        Assert.NotNull(primaryKey);
        Assert.Contains(primaryKey.Properties, p => p.Name == "UserId");
        Assert.Contains(primaryKey.Properties, p => p.Name == "RoleId");
    }

    [Fact]
    public void Configure_ShouldSetRelationshipWithUser()
    {
        // Arrange
        var modelBuilder = GetModelBuilder();
        var configuration = new UserRoleConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<UserRole>());

        // Assert
        var entity = modelBuilder.Model.FindEntityType(typeof(UserRole));
        var navigation = entity!.FindNavigation("User");

        // Check that the relationship with "User" is correctly configured
        Assert.NotNull(navigation);
        Assert.Equal("User", GetSimpleEntityName(navigation.ForeignKey.PrincipalEntityType.Name)); // Ensure relationship to User
        Assert.Equal("UserId", navigation.ForeignKey.Properties[0].Name); // ForeignKey property
        Assert.True(navigation.ForeignKey.IsRequired, "User foreign key should be required"); // Assert IsRequired explicitly
    }

    [Fact]
    public void Configure_ShouldSetRelationshipWithRole()
    {
        // Arrange
        var modelBuilder = GetModelBuilder();
        var configuration = new UserRoleConfiguration();

        // Act
        configuration.Configure(modelBuilder.Entity<UserRole>());

        // Assert
        var entity = modelBuilder.Model.FindEntityType(typeof(UserRole));
        var navigation = entity!.FindNavigation("Role");

        // Check that the relationship with "Role" is correctly configured
        Assert.NotNull(navigation);
        Assert.Equal("Role", GetSimpleEntityName(navigation.ForeignKey.PrincipalEntityType.Name)); // Ensure relationship to Role
        Assert.Equal("RoleId", navigation.ForeignKey.Properties[0].Name); // ForeignKey property
        Assert.True(navigation.ForeignKey.IsRequired, "Role foreign key should be required"); // Assert IsRequired explicitly
    }


    private string GetSimpleEntityName(string fullyQualifiedName)
    {
        var nameParts = fullyQualifiedName.Split('.');
        return nameParts.Length > 0 ? nameParts[^1] : fullyQualifiedName;
    }
}