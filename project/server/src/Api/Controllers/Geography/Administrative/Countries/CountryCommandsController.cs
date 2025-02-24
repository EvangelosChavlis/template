// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Administrative.Countries.Interfaces;
using server.src.Domain.Geography.Administrative.Countries.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Api.Controllers.Geography.Administrative.Countries;

[ApiController]
[Route("api/geography/administrative/countries")]
[Authorize(Roles = "Administrator")]
public class CountryCommandsController : BaseApiController
{
    private readonly ICountryCommands _countryCommands;
    
    public CountryCommandsController(ICountryCommands CountryCommands)
    {
        _countryCommands = CountryCommands;
    }

    [ApiExplorerSettings(GroupName = "geography")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new geography country", Description = "Creates a new geography country in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "country created successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid country data")]
    public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDto dto, CancellationToken token)
    {
        var result = await _countryCommands.CreateCountryAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }
       

    [ApiExplorerSettings(GroupName = "geography")]
    [HttpPost]
    [Route("initialize")]
    [SwaggerOperation(Summary = "Initialize multiple geography countries", Description = "Initializes multiple geography countries in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "countries initialized successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Country data")]
    public async Task<IActionResult> InitializeCountry([FromBody] List<CreateCountryDto> dto, CancellationToken token)
    {
        var result = await _countryCommands.InitializeCountriesAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography")]
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Update an existing geography Country", Description = "Updates an existing geography Country by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Country updated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Country not found")]
    public async Task<IActionResult> UpdateCountry(Guid id, [FromBody] UpdateCountryDto dto, CancellationToken token)
    {
        var result = await _countryCommands.UpdateCountryAsync(id, dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography")]
    [HttpDelete]
    [Route("{id}/{versionId}")]
    [SwaggerOperation(Summary = "Delete a geography country by ID", Description = "Deletes a specific geography country by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Country deleted successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Country not found")]
    public async Task<IActionResult> DeleteCountry(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _countryCommands.DeleteCountryAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }
}