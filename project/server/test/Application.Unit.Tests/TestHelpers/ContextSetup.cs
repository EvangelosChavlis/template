// // packages
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Caching.Memory;


// // source
// using server.src.Persistence.Contexts;
// using server.src.Persistence.Interfaces;
// using server.src.Persistence.Repositories;

// namespace server.test.Application.Unit.Tests.TestHelpers;

// public class ContextSetup
// {
//     public DbContextOptions<DataContext> GetInMemoryDbContextOptions(string dbName)
//     {
//         return new DbContextOptionsBuilder<DataContext>()
//             .UseInMemoryDatabase(databaseName: dbName)
//             .EnableSensitiveDataLogging()
//             .Options;
//     }

//     // Use the custom TestDataContext to simulate the failure scenario
//     public DataContext CreateDbContext(string dbName, bool simulateFailure = false)
//     {
//         var options = GetInMemoryDbContextOptions(dbName);
//         if (simulateFailure)
//         {
//             return new TestDataContext(options); // Return the custom TestDataContext to simulate the failure
//         }
//         return new DataContext(options);
//     }

//     public ICommonRepository CreateCommonRepository() => 
//         new CommonRepository();

//     public async Task ClearDatabase(DataContext context)
//     {
//         var warnings = context.Warnings.ToList();
//         context.Warnings.RemoveRange(warnings);
//         await context.SaveChangesAsync();
//     }

//     public IMemoryCache CreateMemoryCache()
//     {
//         return new MemoryCache(new MemoryCacheOptions());
//     }
// }