// packages
using System.Linq.Expressions;

namespace server.src.Domain.Common.Models;

/// <summary>
/// Represents a model used to define the "Include" and "ThenInclude" relationships 
/// for eager loading related entities in a query, such as when using Entity Framework.
/// This class allows specifying an initial "Include" expression and additional "ThenInclude"
/// expressions to include related data in subsequent levels.
/// </summary>
/// <typeparam name="T">The type of the entity being queried (e.g., User, Product).</typeparam>
public class IncludeThenInclude<T>
{
    /// <summary>
    /// Gets or sets the initial "Include" expression to load related entities.
    /// </summary>
    public Expression<Func<T, object>> Include { get; set; }

    /// <summary>
    /// Gets or sets a list of "ThenInclude" expressions to load additional related entities 
    /// for the included entity.
    /// </summary>
    public List<Expression<Func<object, object>>> ThenIncludes { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IncludeThenInclude{T}"/> class 
    /// with a single "Include" expression.
    /// </summary>
    /// <param name="include">The initial "Include" expression to load related entities.</param>
    public IncludeThenInclude(Expression<Func<T, object>> include)
    {
        Include = include;
        ThenIncludes = new List<Expression<Func<object, object>>>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IncludeThenInclude{T}"/> class 
    /// with an "Include" expression and a list of "ThenInclude" expressions.
    /// </summary>
    /// <param name="include">The initial "Include" expression to load related entities.</param>
    /// <param name="thenIncludes">A list of "ThenInclude" expressions to load additional related entities.</param>
    public IncludeThenInclude(Expression<Func<T, object>> include, List<Expression<Func<object, object>>> thenIncludes)
    {
        Include = include;
        ThenIncludes = thenIncludes;
    }
}
