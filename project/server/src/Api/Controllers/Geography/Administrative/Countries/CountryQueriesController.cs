// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Administrative.Countries.Interfaces;
using server.src.Domain.Common.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Countries.Dtos;

namespace server.src.Api.Controllers.Geography.Administrative.Countries;

[ApiController]
[Route("api/geography/administrative/countries")]
[Authorize(Roles = "Administrator")]
public class CountryQueriesController : BaseApiController
{
    private readonly ICountryQueries _countryQueries;
    
    public CountryQueriesController(ICountryQueries countryQueries)
    {
        _countryQueries = countryQueries;
    }

    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of geography countries", Description = "Retrieves a list of geography countries with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of countries retrieved successfully", typeof(ListResponse<List<ListItemCountryDto>>))]
    public async Task<IActionResult> GetCountries([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _countryQueries.GetCountriesAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific geography country by ID", Description = "Retrieves details of a specific geography country by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Country retrieved successfully", typeof(Response<ItemCountryDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Country not found")]
    public async Task<IActionResult> GetCountryById(Guid id, CancellationToken token)
    {
        var result = await _countryQueries.GetCountryByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }
}