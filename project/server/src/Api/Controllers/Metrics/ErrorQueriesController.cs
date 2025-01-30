// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Metrics;
using server.src.Domain.Models.Common;
using server.src.Application.Metrics.Errors.Interfaces;

namespace server.src.WebApi.Controllers.Metrics;

[ApiController]
[Route("api/metrics/errors")]
[Authorize(Roles = "Administrator")]
public class ErrorQueriesController : BaseApiController
{
    private readonly IErrorQueries _errorQueries;
    
    public ErrorQueriesController(IErrorQueries errorQueries)
    {
        _errorQueries = errorQueries;
    }

    [ApiExplorerSettings(GroupName = "metrics")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of errors", Description = "Retrieves a list of errors with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of errors retrieved successfully", typeof(ListResponse<List<ListItemErrorDto>>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid query parameters")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No errors found")]
    public async Task<IActionResult> GetErrors([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _errorQueries.GetErrorsAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "metrics")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific error by ID", Description = "Retrieves the details of a specific error by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Error retrieved successfully", typeof(Response<ItemErrorDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Error not found")]
    public async Task<IActionResult> GetErrorByIdS(Guid id, CancellationToken token)
    {
        var result = await _errorQueries.GetErrorByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }
}
