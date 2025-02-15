// // packages
// using Microsoft.EntityFrameworkCore;

// // source
// using server.src.Persistence.Extensions;

// namespace server.test.Persistence.Unit.Tests.Extensions;

// public class DbContextExtensionsTests
// {
//     public class TestDbContext : DbContext
//     {
//         public TestDbContext(DbContextOptions options) : base(options) { }

//         public DbSet<TestEntity> TestEntities { get; set; }
//         public DbSet<AnotherEntity> AnotherEntities { get; set; }
//     }

//     public class TestEntity
//     {
//         public int Id { get; set; }
//         public string Name { get; set; }
//     }

//     public class AnotherEntity
//     {
//         public int Id { get; set; }
//         public string Description { get; set; }
//     }

//     private DbContextOptions<TestDbContext> GetInMemoryDbContextOptions()
//     {
//         return new DbContextOptionsBuilder<TestDbContext>()
//             .UseInMemoryDatabase(databaseName: "TestDatabase")
//             .Options;
//     }

//     [Fact]
//     public void GetDbSetName_ShouldReturnCorrectName_ForExistingDbSet()
//     {
//         // Arrange
//         var options = GetInMemoryDbContextOptions();
//         using var context = new TestDbContext(options);

//         // Act
//         var dbSetName = context.GetDbSetName(context.TestEntities);

//         // Assert
//         Assert.Equal("TestEntities", dbSetName);
//     }


//     [Fact]
//     public void GetDbSetName_ShouldReturnCorrectName_ForAnotherDbSet()
//     {
//         // Arrange
//         var options = GetInMemoryDbContextOptions();
//         using var context = new TestDbContext(options);

//         // Act
//         var dbSetName = context.GetDbSetName(context.AnotherEntities);

//         // Assert
//         Assert.Equal("AnotherEntities", dbSetName);
//     }

//     [Fact]
//     public void GetDbSetName_ShouldReturnNull_IfDbSetIsNull()
//     {
//         // Arrange
//         var options = GetInMemoryDbContextOptions();
//         using var context = new TestDbContext(options);

//         // Act
//         var dbSetName = context.GetDbSetName<TestDbContext, TestEntity>(null!);

//         // Assert
//         Assert.Null(dbSetName);
//     }


//     // Supporting DbContext for testing multiple DbSets of the same type
//     public class DbContextWithMultipleDbSets : DbContext
//     {
//         public DbContextWithMultipleDbSets(DbContextOptions options) : base(options) { }

//         public DbSet<TestEntity> FirstEntities { get; set; }
//         public DbSet<TestEntity> SecondEntities { get; set; }
//     }
// }
