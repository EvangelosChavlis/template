// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Administrative.Municipalities.Interfaces;
using server.src.Domain.Common.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Municipalities.Dtos;

namespace server.src.Api.Controllers.Geography.Administrative.Municipalities;

[ApiController]
[Route("api/geography/administrative/municipalities")]
[Authorize(Roles = "Administrator")]
public class MunicipalityQueriesController : BaseApiController
{
    private readonly IMunicipalityQueries _municipalityQueries;
    
    public MunicipalityQueriesController(IMunicipalityQueries municipalityQueries)
    {
        _municipalityQueries = municipalityQueries;
    }

    [ApiExplorerSettings(GroupName = "geography")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of geography municipalities", Description = "Retrieves a list of geography municipalities with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of municipalities retrieved successfully", typeof(ListResponse<List<ListItemMunicipalityDto>>))]
    public async Task<IActionResult> GetMunicipalities([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _municipalityQueries.GetMunicipalitiesAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific geography municipality by ID", Description = "Retrieves details of a specific geography municipality by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Municipality retrieved successfully", typeof(Response<ItemMunicipalityDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Municipality not found")]
    public async Task<IActionResult> GetMunicipalityById(Guid id, CancellationToken token)
    {
        var result = await _municipalityQueries.GetMunicipalityByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }
}