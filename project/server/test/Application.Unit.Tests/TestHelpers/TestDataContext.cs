// // packages
// using Microsoft.EntityFrameworkCore;

// // source
// using server.src.Persistence.Contexts;
// using server.src.Persistence.Interfaces;
// using server.src.Persistence.Repositories;

// namespace server.test.Application.Unit.Tests.TestHelpers;

// public class TestDataContext : DataContext
// {
//     public TestDataContext(DbContextOptions<DataContext> options) 
//         : base(options)
//     { }

//     // Override SaveChangesAsync to simulate failure (returns 0 to indicate no changes saved)
//     public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
//     {
//         // Simulating failure scenario: returning 0 to indicate no changes were saved
//         return await Task.FromResult(0); 
//     }

//     public DbContextOptions<DataContext> GetInMemoryDbContextOptions(string dbName)
//         {
//             return new DbContextOptionsBuilder<DataContext>()
//                 .UseInMemoryDatabase(databaseName: dbName)
//                 .Options;
//         }

//         // Use the custom TestDataContext to simulate the failure scenario
//         public DataContext CreateDbContext(string dbName, bool simulateFailure = false)
//         {
//             var options = GetInMemoryDbContextOptions(dbName);
//             if (simulateFailure)
//             {
//                 return new TestDataContext(options); // Return the custom TestDataContext to simulate the failure
//             }
//             return new DataContext(options);
//         }

//         public ICommonRepository CreateCommonRepository() => 
//             new CommonRepository();


//         public async Task ClearDatabase(DataContext context)
//         {
//             var warnings = context.Warnings.ToList();
//             context.Warnings.RemoveRange(warnings);
//             await context.SaveChangesAsync();
//         }
// }