// packages
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Interfaces.Data;
using server.src.Domain.Dto.Common;

namespace server.src.WebApi.Controllers.Data;

[ApiController]
[Route("api/data")]
// [Authorize(Roles = "Administrator")]
public class DataCommandsController : BaseApiController
{
    private readonly IDataCommands _dataService;
    
    public DataCommandsController(IDataCommands dataService)
    {
        _dataService = dataService;
    }

    [ApiExplorerSettings(GroupName = "data")]
    [HttpGet]
    [Route("seed")]
    [SwaggerOperation(Summary = "Seed database with initial data", Description = "Seeds the database with initial test or setup data.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Data seeded successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Error while seeding data")]
    public async Task<IActionResult> Seed(CancellationToken token)
        => Ok(await _dataService.SeedDataAsync(token));


    [ApiExplorerSettings(GroupName = "data")]
    [HttpGet]
    [Route("clear")]
    [SwaggerOperation(Summary = "Clear all data from the database", Description = "Clears all data from the database.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Data cleared successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Error while clearing data")]
    public async Task<IActionResult> Clear(CancellationToken token)
        => Ok(await _dataService.ClearDataAsync(token));
}
