// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Domain.Models.Common;
using server.src.WebApi.Controllers;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Application.Weather.Warnings.Interfaces;

namespace server.src.Api.Controllers.Weather.Warnings;

[ApiController]
[Route("api/weather/warnings")]
[Authorize(Roles = "Administrator")]
public class WarningQueriesController : BaseApiController
{
    private readonly IWarningQueries _warningQueries;
    
    public WarningQueriesController(IWarningQueries warningQueries)
    {
        _warningQueries = warningQueries;
    }

    [ApiExplorerSettings(GroupName = "weather")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of weather warnings", Description = "Retrieves a list of weather warnings with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of warnings retrieved successfully", typeof(ListResponse<List<ListItemWarningDto>>))]
    public async Task<IActionResult> GetWarnings([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _warningQueries.GetWarningsAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific weather warning by ID", Description = "Retrieves details of a specific weather warning by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Warning retrieved successfully", typeof(Response<ItemWarningDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Warning not found")]
    public async Task<IActionResult> GetWarningById(Guid id, CancellationToken token)
    {
        var result = await _warningQueries.GetWarningByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpGet]
    [Route("picker")]
    [SwaggerOperation(Summary = "Get a list of weather warnings for the picker", Description = "Retrieves a list of weather warnings available for selection in the picker.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of warnings for picker retrieved successfully", typeof(Response<List<PickerWarningDto>>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No warnings found for picker")]
    public async Task<IActionResult> GetWarningsPicker(CancellationToken token)
    {
        var result = await _warningQueries.GetWarningsPickerAsync(token);
        return StatusCode(result.StatusCode, result);
    }
}