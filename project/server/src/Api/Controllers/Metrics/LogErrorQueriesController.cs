// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Metrics.LogErrors.Interfaces;
using server.src.Domain.Common.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Metrics.LogErrors.Dtos;

namespace server.src.Api.Controllers.Metrics;

[ApiController]
[Route("api/metrics/logerrors")]
[Authorize(Roles = "Administrator")]
public class LogErrorQueriesController : BaseApiController
{
    private readonly ILogErrorQueries _logErrorQueries;
    
    public LogErrorQueriesController(ILogErrorQueries errorQueries)
    {
        _logErrorQueries = errorQueries;
    }

    [ApiExplorerSettings(GroupName = "metrics")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of errors", Description = "Retrieves a list of errors with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of errors retrieved successfully", typeof(ListResponse<List<ListItemLogErrorDto>>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid query parameters")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No errors found")]
    public async Task<IActionResult> GetLogErrors([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _logErrorQueries.GetLogErrorsAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "metrics")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific error by ID", Description = "Retrieves the details of a specific error by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Error retrieved successfully", typeof(Response<ItemLogErrorDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Error not found")]
    public async Task<IActionResult> GetErrorByIdS(Guid id, CancellationToken token)
    {
        var result = await _logErrorQueries.GetLogErrorByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }
}
