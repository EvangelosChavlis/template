// // source
// using server.src.Domain.Dto.Auth;

// namespace server.test.Domain.Unit.Tests.Dto.Auth;

// public class RolesDtoTests
// {
//     #region ItemRoleDtoTests
//     [Fact]
//     public void ItemRoleDto_ShouldInitializePropertiesCorrectly()
//     {
//         // Arrange
//         var id = "123";
//         var name = "Manager";
//         var description = "Managerial role";
//         var isActive = true;

//         // Act
//         var itemRoleDto = new ItemRoleDto(id, name, description, isActive);

//         // Assert
//         Assert.Equal(id, itemRoleDto.Id); // Id should match
//         Assert.Equal(name, itemRoleDto.Name); // Name should match
//         Assert.Equal(description, itemRoleDto.Description); // Description should match
//         Assert.Equal(isActive, itemRoleDto.IsActive); // IsActive should match
//     }

//     [Fact]
//     public void ItemRoleDto_ShouldSupportValueEquality()
//     {
//         // Arrange
//         var role1 = new ItemRoleDto("123", "Admin", "Administrative role", true);
//         var role2 = new ItemRoleDto("123", "Admin", "Administrative role", true);

//         // Act & Assert
//         Assert.Equal(role1, role2); // Records should be equal when values match
//         Assert.True(role1 == role2); // Operator should also recognize equality
//     }

//     [Fact]
//     public void ItemRoleDto_ShouldGenerateDifferentHashCodesForDifferentValues()
//     {
//         // Arrange
//         var role1 = new ItemRoleDto("123", "Admin", "Administrative role", true);
//         var role2 = new ItemRoleDto("456", "User", "General user role", false);

//         // Act & Assert
//         Assert.NotEqual(role1.GetHashCode(), role2.GetHashCode()); // Hash codes should differ
//     }
//     #endregion

//     #region RoleDtoTests
//     [Fact]
//     public void RoleDto_ShouldInitializePropertiesCorrectly()
//     {
//         // Arrange
//         var name = "Administrator";
//         var description = "Role with full permissions";

//         // Act
//         var roleDto = new RoleDto(name, description);

//         // Assert
//         Assert.Equal(name, roleDto.Name); // Name should match
//         Assert.Equal(description, roleDto.Description); // Description should match
//     }

//     [Fact]
//     public void RoleDto_ShouldSupportValueEquality()
//     {
//         // Arrange
//         var role1 = new RoleDto("Admin", "Administrative role");
//         var role2 = new RoleDto("Admin", "Administrative role");

//         // Act & Assert
//         Assert.Equal(role1, role2); // Records should be equal when values match
//         Assert.True(role1 == role2); // Operator should also recognize equality
//     }

//     [Fact]
//     public void RoleDto_ShouldGenerateDifferentHashCodesForDifferentValues()
//     {
//         // Arrange
//         var role1 = new RoleDto("Admin", "Administrative role");
//         var role2 = new RoleDto("User", "General user role");

//         // Act & Assert
//         Assert.NotEqual(role1.GetHashCode(), role2.GetHashCode()); // Hash codes should differ
//     }
//     #endregion
// }