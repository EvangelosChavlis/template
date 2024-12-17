// packages
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

// source
using server.src.Domain.Models.Common;
using server.src.Persistence.Contexts;
using server.src.Persistence.Repositories;

namespace server.test.Persistence.Unit.Tests.Repositories;

public class CommonRepositoryTests
{
    private DbContextOptions<DataContext> GetInMemoryDbContextOptions()
    {
        // Use a unique database name for each test to ensure isolation
        return new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    private DataContext GetInMemoryDbContext(DbContextOptions<DataContext> options)
    {
        var context = new InMemoryDataContext(options);
        return context;
    }

    // Custom in-memory DataContext to include TestEntity
    public class InMemoryDataContext : DataContext
    {
        public InMemoryDataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define the TestEntity model for testing
            modelBuilder.Entity<TestEntity>().HasKey(te => te.Id);
        }
    }

    // Helper method to create test data
    private void CreateTestData(DataContext context)
    {
        // Clear any existing data
        context.Set<TestEntity>().RemoveRange(context.Set<TestEntity>());
        context.SaveChanges();

        // Add new test data
        context.Set<TestEntity>().AddRange(
            new TestEntity { Id = 1, Name = "Entity 1" },
            new TestEntity { Id = 2, Name = "Entity 2" },
            new TestEntity { Id = 3, Name = "Entity 3" }
        );
        context.SaveChanges();
    }

    private UrlQuery GetUrlQuery(int pageNumber = 1, int pageSize = 2, string sortBy = "Name", bool sortDescending = false)
    {
        return new UrlQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            SortBy = sortBy,
            SortDescending = sortDescending
        };
    }

    [Fact]
    public async Task GetPagedResultsAsync_ShouldReturnPagedData()
    {
        // Arrange
        var options = GetInMemoryDbContextOptions();
        var context = GetInMemoryDbContext(options);
        CreateTestData(context);

        var repository = new CommonRepository();
        var pageParams = GetUrlQuery(pageNumber: 1, pageSize: 2);

        // Act
        var result = await repository.GetPagedResultsAsync<TestEntity>(
            context.Set<TestEntity>(),
            pageParams,
            filterExpressions: null!,
            includeThenIncludeExpressions: null!,
            CancellationToken.None
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Rows.Count); // Requested 2 items per page
        Assert.Equal(3, result.UrlQuery.TotalRecords); // Total records in DB
        Assert.Equal(1, result.UrlQuery.PageNumber); // On first page
    }

    [Fact]
    public async Task GetPagedResultsAsync_ShouldHandleDifferentPageSizes()
    {
        // Arrange
        var options = GetInMemoryDbContextOptions();
        var context = GetInMemoryDbContext(options);
        CreateTestData(context);

        var repository = new CommonRepository();
        var pageParams = GetUrlQuery(pageNumber: 1, pageSize: 3);

        // Act
        var result = await repository.GetPagedResultsAsync<TestEntity>(
            context.Set<TestEntity>(),
            pageParams,
            filterExpressions: null!,
            includeThenIncludeExpressions: null!,
            CancellationToken.None
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Rows.Count); // All records fit on one page
        Assert.Equal(3, result.UrlQuery.TotalRecords);
        Assert.Equal(1, result.UrlQuery.PageNumber);
    }

    [Fact]
    public async Task GetPagedResultsAsync_ShouldSortDescending()
    {
        // Arrange
        var options = GetInMemoryDbContextOptions();
        var context = GetInMemoryDbContext(options);
        CreateTestData(context);

        var repository = new CommonRepository();
        var pageParams = GetUrlQuery(pageNumber: 1, pageSize: 2, sortBy: "Name", sortDescending: true);

        // Act
        var result = await repository.GetPagedResultsAsync<TestEntity>(
            context.Set<TestEntity>(),
            pageParams,
            filterExpressions: null!,
            includeThenIncludeExpressions: null!,
            CancellationToken.None
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Rows.Count); // Page size is 2
        Assert.Equal("Entity 3", result.Rows[0].Name); // First item
        Assert.Equal("Entity 2", result.Rows[1].Name); // Second item
    }

    [Fact]
    public async Task GetPagedResultsAsync_ShouldApplyFilters()
    {
        // Arrange
        var options = GetInMemoryDbContextOptions();
        var context = GetInMemoryDbContext(options);
        CreateTestData(context);

        var repository = new CommonRepository();
        var pageParams = GetUrlQuery(pageNumber: 1, pageSize: 2);

        var filters = new Expression<Func<TestEntity, bool>>[]
        {
            te => te.Name.Contains("1") || te.Name.Contains("2")
        };

        // Act
        var result = await repository.GetPagedResultsAsync<TestEntity>(
            context.Set<TestEntity>(),
            pageParams,
            filterExpressions: filters,
            includeThenIncludeExpressions: null!,
            CancellationToken.None
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Rows.Count); // Two entities match filter
        Assert.Equal(2, result.UrlQuery.TotalRecords); // Filtered records
        Assert.Contains(result.Rows, e => e.Name == "Entity 1");
        Assert.Contains(result.Rows, e => e.Name == "Entity 2");
    }

    [Fact]
    public async Task GetResultPickerAsync_ShouldReturnAllData()
    {
        // Arrange
        var options = GetInMemoryDbContextOptions();
        using var context = GetInMemoryDbContext(options);
        CreateTestData(context);
        var repository = new CommonRepository();

        // Act
        var result = await repository.GetResultPickerAsync(context.Set<TestEntity>());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
        Assert.Contains(result, e => e.Name == "Entity 1");
        Assert.Contains(result, e => e.Name == "Entity 2");
        Assert.Contains(result, e => e.Name == "Entity 3");
    }

    [Fact]
    public async Task GetResultPickerAsync_ShouldHandleEmptyDbSet()
    {
        // Arrange
        var options = GetInMemoryDbContextOptions();
        using var context = GetInMemoryDbContext(options);
        var repository = new CommonRepository();

        // Act
        var result = await repository.GetResultPickerAsync(context.Set<TestEntity>());

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result); // No data added, so it should be empty
    }

    [Fact]
    public async Task GetResultPickerAsync_ShouldRespectCancellation()
    {
        // Arrange
        var options = GetInMemoryDbContextOptions();
        using var context = GetInMemoryDbContext(options);
        CreateTestData(context);
        var repository = new CommonRepository();

        using var cts = new CancellationTokenSource();
        cts.Cancel(); // Immediately cancel the token

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(async () =>
        {
            await repository.GetResultPickerAsync(context.Set<TestEntity>(), cts.Token);
        });
    }

    [Fact]
    public async Task GetResultByIdAsync_ShouldReturnFilteredResult()
    {
        // Arrange
        var options = GetInMemoryDbContextOptions();
        using var context = GetInMemoryDbContext(options);
        CreateTestData(context);

        var repository = new CommonRepository();

        // Filter to find an entity with Name = "Entity 1"
        var filters = new Expression<Func<TestEntity, bool>>[]
        {
            e => e.Name == "Entity 1"
        };

        // Act
        var result = await repository.GetResultByIdAsync(
            context.Set<TestEntity>(),
            filters,
            includeExpressions: null!,
            CancellationToken.None
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Entity 1", result.Name);
    }

    [Fact]
    public async Task GetResultByIdAsync_ShouldReturnNullIfNoMatch()
    {
        // Arrange
        var options = GetInMemoryDbContextOptions();
        using var context = GetInMemoryDbContext(options);
        CreateTestData(context);

        var repository = new CommonRepository();

        // Filter to find an entity with Name = "NonExistentEntity"
        var filters = new Expression<Func<TestEntity, bool>>[]
        {
            e => e.Name == "NonExistentEntity"
        };

        // Act
        var result = await repository.GetResultByIdAsync(
            context.Set<TestEntity>(),
            filters,
            includeExpressions: null!,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetResultByIdAsync_ShouldApplyIncludes()
    {
        // Arrange
        var options = GetInMemoryDbContextOptions();
        using var context = GetInMemoryDbContext(options);

        // Add test data with related entities
        var parent = new TestEntity { Id = 1, Name = "Parent Entity" };
        context.Set<TestEntity>().Add(parent);

        var child = new TestChildEntity { Id = 1, ParentId = 1, Name = "Child Entity" };
        context.Set<TestChildEntity>().Add(child);

        context.SaveChanges();

        var repository = new CommonRepository();

        // Filter to find the parent entity
        var filters = new Expression<Func<TestEntity, bool>>[]
        {
            e => e.Name == "Parent Entity"
        };

        // Include child entities
        var includes = new Expression<Func<TestEntity, object>>[]
        {
            e => e.Children // Assuming `Children` is a navigation property in TestEntity
        };

        // Act
        var result = await repository.GetResultByIdAsync(
            context.Set<TestEntity>(),
            filters,
            includes,
            CancellationToken.None
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Parent Entity", result.Name);
        Assert.NotNull(result.Children);
        Assert.Single(result.Children);
        Assert.Equal("Child Entity", result.Children.First().Name);
    }

    [Fact]
    public async Task GetResultByIdAsync_ShouldRespectCancellation()
    {
        // Arrange
        var options = GetInMemoryDbContextOptions();
        using var context = GetInMemoryDbContext(options);
        CreateTestData(context);

        var repository = new CommonRepository();

        // Filter to find an entity with Name = "Entity 1"
        var filters = new Expression<Func<TestEntity, bool>>[]
        {
            e => e.Name == "Entity 1"
        };

        using var cts = new CancellationTokenSource();
        cts.Cancel(); // Immediately cancel the token

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(async () =>
        {
            await repository.GetResultByIdAsync(
                context.Set<TestEntity>(),
                filters,
                includeExpressions: null!,
                cts.Token
            );
        });
    }

    [Fact]
    public async Task GetCountAsync_ShouldReturnCorrectCountWithoutFilters()
    {
        // Arrange
        var options = GetInMemoryDbContextOptions();
        using var context = GetInMemoryDbContext(options);
        CreateTestData(context);

        var repository = new CommonRepository();

        // Act
        var count = await repository.GetCountAsync(
            context.Set<TestEntity>(),
            filterExpressions: null!,
            CancellationToken.None
        );

        // Assert
        Assert.Equal(3, count); // Assuming CreateTestData creates 3 entities
    }

    [Fact]
    public async Task GetCountAsync_ShouldReturnCorrectCountWithFilters()
    {
        // Arrange
        var options = GetInMemoryDbContextOptions();
        using var context = GetInMemoryDbContext(options);
        CreateTestData(context);

        var repository = new CommonRepository();

        // Filter to count entities whose Name contains "Entity 1" or "Entity 2"
        var filters = new Expression<Func<TestEntity, bool>>[]
        {
            e => e.Name.Contains("Entity 1") || e.Name.Contains("Entity 2")
        };

        // Act
        var count = await repository.GetCountAsync(
            context.Set<TestEntity>(),
            filters,
            CancellationToken.None
        );

        // Assert
        Assert.Equal(2, count); // Two entities match the filter
    }

    [Fact]
    public async Task GetCountAsync_ShouldReturnZeroIfNoMatches()
    {
        // Arrange
        var options = GetInMemoryDbContextOptions();
        using var context = GetInMemoryDbContext(options);
        CreateTestData(context);

        var repository = new CommonRepository();

        // Filter to count entities with a Name that doesn't exist
        var filters = new Expression<Func<TestEntity, bool>>[]
        {
            e => e.Name.Contains("NonExistent")
        };

        // Act
        var count = await repository.GetCountAsync(
            context.Set<TestEntity>(),
            filters,
            CancellationToken.None
        );

        // Assert
        Assert.Equal(0, count); // No entities match the filter
    }

    [Fact]
    public async Task GetCountAsync_ShouldRespectCancellation()
    {
        // Arrange
        var options = GetInMemoryDbContextOptions();
        using var context = GetInMemoryDbContext(options);
        CreateTestData(context);

        var repository = new CommonRepository();

        using var cts = new CancellationTokenSource();
        cts.Cancel(); // Immediately cancel the token

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(async () =>
        {
            await repository.GetCountAsync(
                context.Set<TestEntity>(),
                filterExpressions: null!,
                cts.Token
            );
        });
    }

    [Fact]
    public async Task GetCountAsync_ShouldHandleMultipleFilters()
    {
        // Arrange
        var options = GetInMemoryDbContextOptions();
        using var context = GetInMemoryDbContext(options);
        CreateTestData(context);

        var repository = new CommonRepository();

        // Multiple filters to find a specific entity
        var filters = new Expression<Func<TestEntity, bool>>[]
        {
            e => e.Name.Contains("Entity"),
            e => e.Name.Contains("1")
        };

        // Act
        var count = await repository.GetCountAsync(
            context.Set<TestEntity>(),
            filters,
            CancellationToken.None
        );

        // Assert
        Assert.Equal(1, count); // Only "Entity 1" matches both filters
    }


    public class TestEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TestChildEntity> Children { get; set; } = new List<TestChildEntity>();
    }

    public class TestChildEntity
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public TestEntity Parent { get; set; }
    }
}
