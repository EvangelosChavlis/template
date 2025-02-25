// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Administrative.Districts.Interfaces;
using server.src.Domain.Common.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Districts.Dtos;

namespace server.src.Api.Controllers.Geography.Administrative.Districts;

[ApiController]
[Route("api/geography/administrative/districts")]
[Authorize(Roles = "Administrator")]
public class DistrictQueriesController : BaseApiController
{
    private readonly IDistrictQueries _districtQueries;
    
    public DistrictQueriesController(IDistrictQueries districtQueries)
    {
        _districtQueries = districtQueries;
    }

    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of geography districts", Description = "Retrieves a list of geography districts with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of districts retrieved successfully", typeof(ListResponse<List<ListItemDistrictDto>>))]
    public async Task<IActionResult> GetDistricts([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _districtQueries.GetDistrictsAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific geography district by ID", Description = "Retrieves details of a specific geography district by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "District retrieved successfully", typeof(Response<ItemDistrictDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "District not found")]
    public async Task<IActionResult> GetDistrictById(Guid id, CancellationToken token)
    {
        var result = await _districtQueries.GetDistrictByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }
}