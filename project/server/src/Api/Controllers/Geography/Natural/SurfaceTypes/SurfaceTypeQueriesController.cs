// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Natural.SurfaceTypes.Interfaces;
using server.src.Domain.Common.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;

namespace server.src.Api.Controllers.Geography.Natural.SurfaceTypes;

[ApiController]
[Route("api/geography/natural/surfaceTypes")]
[Authorize(Roles = "Administrator")]
public class SurfaceTypeQueriesController : BaseApiController
{
    private readonly ISurfaceTypeQueries _surfaceTypeQueries;
    
    public SurfaceTypeQueriesController(ISurfaceTypeQueries surfaceTypeQueries)
    {
        _surfaceTypeQueries = surfaceTypeQueries;
    }

    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of geography surface types", Description = "Retrieves a list of geography SurfaceTypes with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of SurfaceTypes retrieved successfully", typeof(ListResponse<List<ListItemSurfaceTypeDto>>))]
    public async Task<IActionResult> GetSurfaceTypes([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _surfaceTypeQueries.GetSurfaceTypesAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific geography surface type by ID", Description = "Retrieves details of a specific geography SurfaceType by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "SurfaceType retrieved successfully", typeof(Response<ItemSurfaceTypeDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "SurfaceType not found")]
    public async Task<IActionResult> GetSurfaceTypeById(Guid id, CancellationToken token)
    {
        var result = await _surfaceTypeQueries.GetSurfaceTypeByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpGet]
    [Route("picker")]
    [SwaggerOperation(Summary = "Get a list of geography surface types for the picker", Description = "Retrieves a list of geography SurfaceTypes available for selection in the picker.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of SurfaceTypes for picker retrieved successfully", typeof(Response<List<PickerSurfaceTypeDto>>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No SurfaceTypes found for picker")]
    public async Task<IActionResult> GetSurfaceTypesPicker(CancellationToken token)
    {
        var result = await _surfaceTypeQueries.GetSurfaceTypesPickerAsync(token);
        return StatusCode(result.StatusCode, result);
    }
}