namespace server.src.Domain.Models.Errors;

public class ValidationResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the validation was successful.
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Gets or sets the list of error messages, if any, that occurred during validation.
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Creates a new <see cref="ValidationResult"/> indicating a successful validation.
    /// </summary>
    /// <returns>A <see cref="ValidationResult"/> with <see cref="IsValid"/> set to true.</returns>
    public static ValidationResult Success() 
        => new() 
        {
            IsValid = true 
        };

    /// <summary>
    /// Creates a new <see cref="ValidationResult"/> indicating a failed validation with a list of errors.
    /// </summary>
    /// <param name="errors">A collection of error messages describing the validation failures.</param>
    /// <returns>A <see cref="ValidationResult"/> with <see cref="IsValid"/> set to false and the provided error messages.</returns>
    public static ValidationResult Failure(IEnumerable<string> errors) 
        => new() 
        { 
            IsValid = false, 
            Errors = errors.ToList() 
        };
}
