// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;

namespace server.src.Application.Geography.Natural.NaturalFeatures.Interfaces;

/// <summary>
/// Defines methods for managing geography natural features, including creation, updating, and deletion.
/// </summary>
public interface INaturalFeatureCommands
{
    /// <summary>
    /// Initializes multiple geography natural features in bulk.
    /// </summary>
    /// <param name="dto">A list of NaturalFeature data transfer objects to be created.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the success or failure of the bulk initialization process.</returns>
    Task<Response<string>> InitializeNaturalFeaturesAsync(List<CreateNaturalFeatureDto> dto, 
        CancellationToken token = default);

    /// <summary>
    /// Creates a new geography NaturalFeature.
    /// </summary>
    /// <param name="dto">The data transfer object containing NaturalFeature details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the operation.</returns>
    Task<Response<string>> CreateNaturalFeatureAsync(CreateNaturalFeatureDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Updates an existing geography NaturalFeature.
    /// </summary>
    /// <param name="id">The unique identifier of the NaturalFeature to update.</param>
    /// <param name="dto">The data transfer object containing updated NaturalFeature details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the update operation.</returns>
    Task<Response<string>> UpdateNaturalFeatureAsync(Guid id, UpdateNaturalFeatureDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Activates a NaturalFeature by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the NaturalFeature to activate.</param>
    /// <param name="version">The version of the NaturalFeature for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the activation was successful.</returns>
    Task<Response<string>> ActivateNaturalFeatureAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deactivates a NaturalFeature by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the NaturalFeature to deactivate.</param>
    /// <param name="version">The version of the NaturalFeature for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the deactivation was successful.</returns>
    Task<Response<string>> DeactivateNaturalFeatureAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deletes a geography NaturalFeature by its unique identifier and version.
    /// </summary>
    /// <param name="id">The unique identifier of the geography NaturalFeature to delete.</param>
    /// <param name="version">The version of the geography NaturalFeature to ensure concurrency control during deletion.</param>
    /// <param name="token">An optional cancellation token to cancel the operation if needed.</param>
    /// <returns>A response containing the result message of the deletion operation, including success or failure details.</returns>
    Task<Response<string>> DeleteNaturalFeatureAsync(Guid id, 
        Guid version, CancellationToken token = default);
}
