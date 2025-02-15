// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Weather.MoonPhases.Interfaces;
using server.src.Domain.Common.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.MoonPhases.Dtos;

namespace server.src.Api.Controllers.Weather.MoonPhases;

[ApiController]
[Route("api/weather/moonPhases")]
[Authorize(Roles = "Administrator")]
public class MoonPhaseQueriesController : BaseApiController
{
    private readonly IMoonPhaseQueries _moonphaseQueries;
    
    public MoonPhaseQueriesController(IMoonPhaseQueries moonphaseQueries)
    {
        _moonphaseQueries = moonphaseQueries;
    }

    [ApiExplorerSettings(GroupName = "weather")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of weather moonphases", Description = "Retrieves a list of weather moonphases with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of moonphases retrieved successfully", typeof(ListResponse<List<ListItemMoonPhaseDto>>))]
    public async Task<IActionResult> GetMoonPhases([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _moonphaseQueries.GetMoonPhasesAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific weather moonphase by ID", Description = "Retrieves details of a specific weather moonphase by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "MoonPhase retrieved successfully", typeof(Response<ItemMoonPhaseDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "MoonPhase not found")]
    public async Task<IActionResult> GetMoonPhaseById(Guid id, CancellationToken token)
    {
        var result = await _moonphaseQueries.GetMoonPhaseByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpGet]
    [Route("picker")]
    [SwaggerOperation(Summary = "Get a list of weather moonphases for the picker", Description = "Retrieves a list of weather moonphases available for selection in the picker.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of moonphases for picker retrieved successfully", typeof(Response<List<PickerMoonPhaseDto>>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No moonphases found for picker")]
    public async Task<IActionResult> GetMoonPhasesPicker(CancellationToken token)
    {
        var result = await _moonphaseQueries.GetMoonPhasesPickerAsync(token);
        return StatusCode(result.StatusCode, result);
    }
}