// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Natural.NaturalFeatures.Interfaces;
using server.src.Domain.Common.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;

namespace server.src.Api.Controllers.Geography.Natural.NaturalFeatures;

[ApiController]
[Route("api/geography/natural/naturalFeatures")]
[Authorize(Roles = "Administrator")]
public class NaturalFeatureQueriesController : BaseApiController
{
    private readonly INaturalFeatureQueries _naturalFeatureQueries;
    
    public NaturalFeatureQueriesController(INaturalFeatureQueries NaturalFeatureQueries)
    {
        _naturalFeatureQueries = NaturalFeatureQueries;
    }

    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of geography natural features", Description = "Retrieves a list of geography NaturalFeatures with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of NaturalFeatures retrieved successfully", typeof(ListResponse<List<ListItemNaturalFeatureDto>>))]
    public async Task<IActionResult> GetNaturalFeatures([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _naturalFeatureQueries.GetNaturalFeaturesAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific geography natural feature by ID", Description = "Retrieves details of a specific geography NaturalFeature by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "NaturalFeature retrieved successfully", typeof(Response<ItemNaturalFeatureDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "NaturalFeature not found")]
    public async Task<IActionResult> GetNaturalFeatureById(Guid id, CancellationToken token)
    {
        var result = await _naturalFeatureQueries.GetNaturalFeatureByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpGet]
    [Route("picker")]
    [SwaggerOperation(Summary = "Get a list of geography natural features for the picker", Description = "Retrieves a list of geography NaturalFeatures available for selection in the picker.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of NaturalFeatures for picker retrieved successfully", typeof(Response<List<PickerNaturalFeatureDto>>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No NaturalFeatures found for picker")]
    public async Task<IActionResult> GetNaturalFeaturesPicker(CancellationToken token)
    {
        var result = await _naturalFeatureQueries.GetNaturalFeaturesPickerAsync(token);
        return StatusCode(result.StatusCode, result);
    }
}