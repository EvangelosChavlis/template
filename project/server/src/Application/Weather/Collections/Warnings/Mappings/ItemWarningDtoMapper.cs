// source
using server.src.Domain.Weather.Collections.Warnings.Dtos;
using server.src.Domain.Weather.Collections.Warnings.Models;

namespace server.src.Application.Weather.Collections.Warnings.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Warning"/> model  
/// into an <see cref="ItemWarningDto"/> for detailed item representation.
/// </summary>
public static class ItemWarningDtoMapper
{
    /// <summary>
    /// Maps a Warning model to an ItemWarningDto.
    /// </summary>
    /// <param name="model">The Warning model that will be mapped to an ItemWarningDto.</param>
    /// <returns>An ItemWarningDto representing the Warning model with full details for an individual item view.</returns>
    public static ItemWarningDto ItemWarningDtoMapping(
        this Warning model) => new(
            Id: model.Id,
            Name: model.Name,
            Description: model.Description,
            Code: model.Code,
            RecommendedActions: model.RecommendedActions,
            IsActive: model.IsActive,
            Version: model.Version
        );
}