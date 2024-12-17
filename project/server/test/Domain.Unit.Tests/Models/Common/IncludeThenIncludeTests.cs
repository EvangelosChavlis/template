// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Models.Common;

namespace server.tests.Domain.Models.Common;

public class IncludeThenIncludeTests
{
    public class TestClass
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    [Fact]
    public void IncludeThenInclude_Constructor_SetsIncludeProperty()
    {
        // Arrange
        Expression<Func<TestClass, object>> includeExpression = x => x.Name;

        // Act
        var includeThenInclude = new IncludeThenInclude<TestClass>(includeExpression);

        // Assert
        Assert.NotNull(includeThenInclude.Include);
        Assert.Equal("x => x.Name", includeThenInclude.Include.ToString());
    }

    [Fact]
    public void IncludeThenInclude_Constructor_SetsThenIncludesToEmptyList()
    {
        // Arrange
        Expression<Func<TestClass, object>> includeExpression = x => x.Name;

        // Act
        var includeThenInclude = new IncludeThenInclude<TestClass>(includeExpression);

        // Assert
        Assert.NotNull(includeThenInclude.ThenIncludes);
        Assert.Empty(includeThenInclude.ThenIncludes);
    }

    [Fact]
    public void IncludeThenInclude_Constructor_SetsIncludeAndThenIncludes()
    {
        // Arrange
        Expression<Func<TestClass, object>> includeExpression = x => x.Name;
        var thenIncludes = new List<Expression<Func<object, object>>>
        {
            CreateThenIncludeExpression()
        };

        // Act
        var includeThenInclude = new IncludeThenInclude<TestClass>(includeExpression, thenIncludes);

        // Assert
        Assert.NotNull(includeThenInclude.Include);
        Assert.Equal("x => x.Name", includeThenInclude.Include.ToString());
        Assert.NotNull(includeThenInclude.ThenIncludes);
        Assert.Single(includeThenInclude.ThenIncludes);

        // Check that the first ThenInclude expression is accessing Age on TestClass
        var thenInclude = includeThenInclude.ThenIncludes[0];
        var body = (MemberExpression)((UnaryExpression)thenInclude.Body).Operand; // Access the MemberExpression (Age)
        Assert.Equal("Age", body.Member.Name);
        Assert.Equal(typeof(TestClass), body.Member.DeclaringType);
    }

    [Fact]
    public void IncludeThenInclude_ThenIncludes_AddsThenIncludeCorrectly()
    {
        // Arrange
        Expression<Func<TestClass, object>> includeExpression = x => x.Name;
        var includeThenInclude = new IncludeThenInclude<TestClass>(includeExpression);

        // Act
        var thenIncludeExpression = CreateThenIncludeExpression();
        includeThenInclude.ThenIncludes.Add(thenIncludeExpression);

        // Assert
        Assert.Single(includeThenInclude.ThenIncludes);

        // Check that the first ThenInclude expression is accessing Age on TestClass
        var thenInclude = includeThenInclude.ThenIncludes[0];
        var body = (MemberExpression)((UnaryExpression)thenInclude.Body).Operand; // Access the MemberExpression (Age)
        Assert.Equal("Age", body.Member.Name);
        Assert.Equal(typeof(TestClass), body.Member.DeclaringType);
    }

    [Fact]
    public void IncludeThenInclude_ThenIncludes_CanBeCleared()
    {
        // Arrange
        Expression<Func<TestClass, object>> includeExpression = x => x.Name;
        var includeThenInclude = new IncludeThenInclude<TestClass>(includeExpression);

        // Act
        var thenIncludeExpression = CreateThenIncludeExpression();
        includeThenInclude.ThenIncludes.Add(thenIncludeExpression);
        includeThenInclude.ThenIncludes.Clear();

        // Assert
        Assert.Empty(includeThenInclude.ThenIncludes);
    }

    // Helper method to create ThenInclude expressions
    private Expression<Func<object, object>> CreateThenIncludeExpression()
    {
        return x => ((TestClass)x).Age;
    }
}