// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Natural.TerrainTypes.Interfaces;
using server.src.Domain.Common.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.TerrainTypes.Dtos;

namespace server.src.Api.Controllers.Geography.Natural.TerrainTypes;

[ApiController]
[Route("api/geography/natural/terrainTypes")]
[Authorize(Roles = "Administrator")]
public class TerrainTypeQueriesController : BaseApiController
{
    private readonly ITerrainTypeQueries _terraintypeQueries;
    
    public TerrainTypeQueriesController(ITerrainTypeQueries terraintypeQueries)
    {
        _terraintypeQueries = terraintypeQueries;
    }

    [ApiExplorerSettings(GroupName = "geography")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of geography terraintypes", Description = "Retrieves a list of geography terraintypes with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of terraintypes retrieved successfully", typeof(ListResponse<List<ListItemTerrainTypeDto>>))]
    public async Task<IActionResult> GetTerrainTypes([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _terraintypeQueries.GetTerrainTypesAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific geography terraintype by ID", Description = "Retrieves details of a specific geography terraintype by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "TerrainType retrieved successfully", typeof(Response<ItemTerrainTypeDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "TerrainType not found")]
    public async Task<IActionResult> GetTerrainTypeById(Guid id, CancellationToken token)
    {
        var result = await _terraintypeQueries.GetTerrainTypeByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography")]
    [HttpGet]
    [Route("picker")]
    [SwaggerOperation(Summary = "Get a list of geography terraintypes for the picker", Description = "Retrieves a list of geography terraintypes available for selection in the picker.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of terraintypes for picker retrieved successfully", typeof(Response<List<PickerTerrainTypeDto>>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No terraintypes found for picker")]
    public async Task<IActionResult> GetTerrainTypesPicker(CancellationToken token)
    {
        var result = await _terraintypeQueries.GetTerrainTypesPickerAsync(token);
        return StatusCode(result.StatusCode, result);
    }
}