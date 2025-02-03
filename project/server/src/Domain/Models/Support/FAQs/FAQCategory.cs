namespace server.src.Domain.Models.Changes;

/// <summary>
/// Represents a category for FAQs, grouping related questions together.
/// </summary>
public class FAQCategory
{
    /// <summary>
    /// Unique identifier for the FAQ category.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the FAQ category (e.g.,"General", "Account", "Billing", "Technical Support").
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// A brief description of the FAQ category.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Indicates whether the category is active and should be displayed.
    /// </summary>
    public bool IsActive { get; set; }

    #region Navigation properties

    /// <summary>
    /// List of FAQs that belong to this category.
    /// </summary>
    public List<FAQ> FAQs { get; set; }
    
    #endregion
}