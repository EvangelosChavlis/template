// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Natural.Locations.Interfaces;
using server.src.Domain.Common.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.Locations.Dtos;

namespace server.src.Api.Controllers.Geography.Natural.Locations;

[ApiController]
[Route("api/geography/natural/locations")]
[Authorize(Roles = "Administrator")]
public class LocationQueriesController : BaseApiController
{
    private readonly ILocationQueries _locationQueries;
    
    public LocationQueriesController(ILocationQueries locationQueries)
    {
        _locationQueries = locationQueries;
    }

    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of geography locations", Description = "Retrieves a list of geography locations with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of locations retrieved successfully", typeof(ListResponse<List<ListItemLocationDto>>))]
    public async Task<IActionResult> GetLocations([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _locationQueries.GetLocationsAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific geography location by ID", Description = "Retrieves details of a specific geography location by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Location retrieved successfully", typeof(Response<ItemLocationDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Location not found")]
    public async Task<IActionResult> GetLocationById(Guid id, CancellationToken token)
    {
        var result = await _locationQueries.GetLocationByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }
}