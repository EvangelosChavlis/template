// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Administrative.Continents.Interfaces;
using server.src.Domain.Common.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Dtos;

namespace server.src.Api.Controllers.Geography.Administrative.Continents;

[ApiController]
[Route("api/geography/administrative/continents")]
[Authorize(Roles = "Administrator")]
public class ContinentQueriesController : BaseApiController
{
    private readonly IContinentQueries _continentQueries;
    
    public ContinentQueriesController(IContinentQueries continentQueries)
    {
        _continentQueries = continentQueries;
    }

    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of geography continents", Description = "Retrieves a list of geography continents with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of continents retrieved successfully", typeof(ListResponse<List<ListItemContinentDto>>))]
    public async Task<IActionResult> GetContinents([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _continentQueries.GetContinentsAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific geography continent by ID", Description = "Retrieves details of a specific geography continent by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Continent retrieved successfully", typeof(Response<ItemContinentDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Continent not found")]
    public async Task<IActionResult> GetContinentById(Guid id, CancellationToken token)
    {
        var result = await _continentQueries.GetContinentByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }
}