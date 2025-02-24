// // source
// using server.src.Domain.Models.Auth;

// namespace server.test.Domain.Unit.Tests.Models.Auth;

// public class RoleTests
// {
//     [Fact]
//     public void Role_ShouldInitializeProperties()
//     {
//         // Arrange & Act
//         var role = new Role { UserRoles = new List<UserRole>() }; // Explicitly initialize UserRoles

//         // Assert
//         Assert.NotNull(role.Id); // Id is set by IdentityRole
//         Assert.Null(role.Name); // Default Name is null
//         Assert.Null(role.Description); // Default Description is null
//         Assert.False(role.IsActive); // Default IsActive is false
//         Assert.NotNull(role.UserRoles); // UserRoles should not be null
//         Assert.Empty(role.UserRoles); // UserRoles should be an empty list
//     }

//     [Fact]
//     public void Role_ShouldSetProperties()
//     {
//         // Arrange
//         var userRole = new UserRole(); // Assuming UserRole is defined elsewhere
//         var role = new Role
//         {
//             Id = "admin",
//             Name = "Administrator",
//             Description = "Role with full permissions",
//             IsActive = true,
//             UserRoles = new List<UserRole> { userRole }
//         };

//         // Act & Assert
//         Assert.Equal("admin", role.Id); // Id should be set correctly
//         Assert.Equal("Administrator", role.Name); // Name should be set correctly
//         Assert.Equal("Role with full permissions", role.Description); // Description should be set correctly
//         Assert.True(role.IsActive); // IsActive should be true
//         Assert.Single(role.UserRoles); // UserRoles should contain one item
//         Assert.Contains(userRole, role.UserRoles); // The correct UserRole should be in the list
//     }

//     [Fact]
//     public void Role_UserRoles_ShouldBeIndependent()
//     {
//         // Arrange
//         var role = new Role { UserRoles = new List<UserRole>() }; // Initialize UserRoles

//         // Act
//         role.UserRoles.Add(new UserRole()); // Modifying the list

//         // Assert
//         Assert.NotEmpty(role.UserRoles); // The list should now contain an item
//     }
// }