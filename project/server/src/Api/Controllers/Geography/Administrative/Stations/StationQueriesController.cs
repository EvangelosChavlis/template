// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Administrative.Stations.Interfaces;
using server.src.Domain.Common.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Stations.Dtos;

namespace server.src.Api.Controllers.Geography.Administrative.Stations;

[ApiController]
[Route("api/geography/administrative/stations")]
[Authorize(Roles = "Administrator")]
public class StationQueriesController : BaseApiController
{
    private readonly IStationQueries _stationQueries;
    
    public StationQueriesController(IStationQueries stationQueries)
    {
        _stationQueries = stationQueries;
    }

    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of geography stations", Description = "Retrieves a list of geography stations with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of stations retrieved successfully", typeof(ListResponse<List<ListItemStationDto>>))]
    public async Task<IActionResult> GetStations([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _stationQueries.GetStationsAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific geography station by ID", Description = "Retrieves details of a specific geography station by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Station retrieved successfully", typeof(Response<ItemStationDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Station not found")]
    public async Task<IActionResult> GetStationById(Guid id, CancellationToken token)
    {
        var result = await _stationQueries.GetStationByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }
}