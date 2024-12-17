// packages
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Interfaces.Metrics;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Metrics;
using server.src.Domain.Models.Common;

namespace server.src.WebApi.Controllers.Metrics;

[ApiController]
[Route("api/metrics/errors")]
public class ErrorsController : BaseApiController
{
    private readonly IErrorsService _errorsService;
    
    public ErrorsController(IErrorsService errorsService)
    {
        _errorsService = errorsService;
    }

    [ApiExplorerSettings(GroupName = "metrics")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of errors", Description = "Retrieves a list of errors with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of errors retrieved successfully", typeof(ListResponse<List<ListItemErrorDto>>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid query parameters")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No errors found")]
    public async Task<IActionResult> GetErrors([FromQuery] UrlQuery urlQuery, CancellationToken token)
        => Ok(await _errorsService.GetErrorsService(urlQuery, token));


    [ApiExplorerSettings(GroupName = "metrics")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific error by ID", Description = "Retrieves the details of a specific error by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Error retrieved successfully", typeof(ItemResponse<ItemErrorDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Error not found")]
    public async Task<IActionResult> GetErrorByIdS(Guid id, CancellationToken token)
        => Ok(await _errorsService.GetErrorByIdService(id, token));
}
