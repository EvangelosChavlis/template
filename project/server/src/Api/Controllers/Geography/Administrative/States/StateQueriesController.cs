// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Administrative.States.Interfaces;
using server.src.Domain.Common.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.States.Dtos;

namespace server.src.Api.Controllers.Geography.Administrative.States;

[ApiController]
[Route("api/geography/administrative/states")]
[Authorize(Roles = "Administrator")]
public class StateQueriesController : BaseApiController
{
    private readonly IStateQueries _stateQueries;
    
    public StateQueriesController(IStateQueries stateQueries)
    {
        _stateQueries = stateQueries;
    }

    [ApiExplorerSettings(GroupName = "geography")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of geography states", Description = "Retrieves a list of geography states with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of states retrieved successfully", typeof(ListResponse<List<ListItemStateDto>>))]
    public async Task<IActionResult> GetStates([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _stateQueries.GetStatesAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific geography state by ID", Description = "Retrieves details of a specific geography state by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "State retrieved successfully", typeof(Response<ItemStateDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "State not found")]
    public async Task<IActionResult> GetStateById(Guid id, CancellationToken token)
    {
        var result = await _stateQueries.GetStateByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }
}