// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Administrative.Neighborhoods.Interfaces;
using server.src.Domain.Common.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;

namespace server.src.Api.Controllers.Geography.Administrative.Neighborhoods;

[ApiController]
[Route("api/geography/administrative/regions")]
[Authorize(Roles = "Administrator")]
public class NeighborhoodQueriesController : BaseApiController
{
    private readonly INeighborhoodQueries _neighborhoodsQueries;
    
    public NeighborhoodQueriesController(INeighborhoodQueries neighborhoodsQueries)
    {
        _neighborhoodsQueries = neighborhoodsQueries;
    }

    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of geography neighborhoods", Description = "Retrieves a list of geography regions with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of regions retrieved successfully", typeof(ListResponse<List<ListItemNeighborhoodDto>>))]
    public async Task<IActionResult> GetNeighborhoods([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _neighborhoodsQueries.GetNeighborhoodsAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific geography neighborhood by ID", Description = "Retrieves details of a specific geography region by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Neighborhood retrieved successfully", typeof(Response<ItemNeighborhoodDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Neighborhood not found")]
    public async Task<IActionResult> GetNeighborhoodById(Guid id, CancellationToken token)
    {
        var result = await _neighborhoodsQueries.GetNeighborhoodByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }
}