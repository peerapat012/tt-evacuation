using Microsoft.AspNetCore.Mvc;
using tt_api.Dtos;
using tt_api.Models;
using tt_api.Services;

namespace tt_api.Controllers;

[Route("api/vehicles")]
[ApiController]
public class VehicleController(IVehicleService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<VehicalDto>>> Get()
    {
        return await Task.FromResult(StatusCode(200, await service.GetVehicals()));
    }

    [HttpPost]
    public async Task<ActionResult> createVehicle(
        [FromBody] List<CreateVehicleDto> createVehicleDtos)
    {
        var results = await service.CreateVehicles(createVehicleDtos);
        return await Task.FromResult(StatusCode(201, results));
    }
}