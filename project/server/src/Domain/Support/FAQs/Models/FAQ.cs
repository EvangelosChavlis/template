// source
using server.src.Domain.Support.FAQCategories;

namespace server.src.Domain.Support.FAQs.Models;

/// <summary>
/// Represents a Frequently Asked Question (FAQ).
/// </summary>
public class FAQ
{
    /// <summary>
    /// Unique identifier for the FAQ.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The question being asked.
    /// </summary>
    public string Question { get; set; }

    /// <summary>
    /// The answer to the question.
    /// </summary>
    public string Answer { get; set; }

    /// <summary>
    /// The number of times this FAQ has been viewed.
    /// </summary>
    public int ViewCount { get; set; }

    /// <summary>
    /// Indicates if the FAQ is active or inactive.
    /// </summary>
    public bool IsActive { get; set; }

    #region Foreign keys

    /// <summary>
    /// Gets or sets the identifier for the associated FAQ Category.
    /// </summary>
    public Guid FAQCategoryId { get; set; }
    
    #endregion

    #region Navigation properties

    /// <summary>
    /// Category of the FAQ (e.g.,"General", "Billing", "Technical Support").
    /// </summary>
    public virtual FAQCategory FAQCategory { get; set; }

    #endregion
}