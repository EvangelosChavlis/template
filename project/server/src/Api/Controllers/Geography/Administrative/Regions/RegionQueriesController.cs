// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Administrative.Regions.Interfaces;
using server.src.Domain.Common.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Regions.Dtos;

namespace server.src.Api.Controllers.Geography.Administrative.Regions;

[ApiController]
[Route("api/geography/administrative/regions")]
[Authorize(Roles = "Administrator")]
public class RegionQueriesController : BaseApiController
{
    private readonly IRegionQueries _regionQueries;
    
    public RegionQueriesController(IRegionQueries regionQueries)
    {
        _regionQueries = regionQueries;
    }

    [ApiExplorerSettings(GroupName = "geography")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of geography regions", Description = "Retrieves a list of geography regions with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of regions retrieved successfully", typeof(ListResponse<List<ListItemRegionDto>>))]
    public async Task<IActionResult> GetRegions([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _regionQueries.GetRegionsAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific geography region by ID", Description = "Retrieves details of a specific geography region by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Region retrieved successfully", typeof(Response<ItemRegionDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Region not found")]
    public async Task<IActionResult> GetRegionById(Guid id, CancellationToken token)
    {
        var result = await _regionQueries.GetRegionByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }
}