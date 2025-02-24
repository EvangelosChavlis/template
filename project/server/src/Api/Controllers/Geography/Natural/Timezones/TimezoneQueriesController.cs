// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Natural.Timezones.Interfaces;
using server.src.Domain.Common.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.Timezones.Dtos;

namespace server.src.Api.Controllers.Geography.Natural.Timezones;

[ApiController]
[Route("api/geography/natural/timezones")]
[Authorize(Roles = "Administrator")]
public class TimezoneQueriesController : BaseApiController
{
    private readonly ITimezoneQueries _timezoneQueries;
    
    public TimezoneQueriesController(ITimezoneQueries timezoneQueries)
    {
        _timezoneQueries = timezoneQueries;
    }

    [ApiExplorerSettings(GroupName = "geography")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of geography timezones", Description = "Retrieves a list of geography timezones with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of timezones retrieved successfully", typeof(ListResponse<List<ListItemTimezoneDto>>))]
    public async Task<IActionResult> GetTimezones([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _timezoneQueries.GetTimezonesAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific geography timezone by ID", Description = "Retrieves details of a specific geography timezone by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Timezone retrieved successfully", typeof(Response<ItemTimezoneDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Timezone not found")]
    public async Task<IActionResult> GetTimezoneById(Guid id, CancellationToken token)
    {
        var result = await _timezoneQueries.GetTimezoneByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography")]
    [HttpGet]
    [Route("picker")]
    [SwaggerOperation(Summary = "Get a list of geography timezones for the picker", Description = "Retrieves a list of geography timezones available for selection in the picker.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of timezones for picker retrieved successfully", typeof(Response<List<PickerTimezoneDto>>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No timezones found for picker")]
    public async Task<IActionResult> GetTimezonesPicker(CancellationToken token)
    {
        var result = await _timezoneQueries.GetTimezonesPickerAsync(token);
        return StatusCode(result.StatusCode, result);
    }
}