// packages
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Data.Interfaces;
using server.src.Domain.Common.Dtos;

namespace server.src.Api.Controllers.Data;

[ApiController]
[Route("api/data")]
// [Authorize(Roles = "Administrator")]
public class DataCommandsController : BaseApiController
{
    private readonly IDataCommands _dataCommands;
    
    public DataCommandsController(IDataCommands dataCommands)
    {
        _dataCommands = dataCommands;
    }

    [ApiExplorerSettings(GroupName = "data")]
    [HttpGet]
    [Route("seed")]
    [SwaggerOperation(Summary = "Seed database with initial data", Description = "Seeds the database with initial test or setup data.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Data seeded successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Error while seeding data")]
    public async Task<IActionResult> Seed(CancellationToken token)
    {
        var result = await _dataCommands.SeedDataAsync(token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "data")]
    [HttpGet]
    [Route("clear")]
    [SwaggerOperation(Summary = "Clear all data from the database", Description = "Clears all data from the database.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Data cleared successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Error while clearing data")]
    public async Task<IActionResult> Clear(CancellationToken token)
    {
        var result = await _dataCommands.ClearDataAsync(token);
        return StatusCode(result.StatusCode, result);
    }
}