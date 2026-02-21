using Microsoft.AspNetCore.Mvc;
using tt_api.Dtos;
using tt_api.Services;
using tt_api.Utils;

namespace tt_api.Controllers;

[Route("api/")]
[ApiController]
public class ZoneController(IEvacuationService service) : ControllerBase
{
    [HttpGet("evacuation/status")]
    public async Task<ActionResult<List<EvacuationZoneStatusDto>>> Get()
    {
        var result = await service.GetEvacZoneStatus();
        return await Task.FromResult(StatusCode(200, result));
    }

    [HttpPost("evacuation-zones")]
    public async Task<ActionResult> CreateEvacuationZones([FromBody] List<CreateEvacuationZoneDto> evacZonesDto)
    {
        var result = await service.CreateEvacuationZone(evacZonesDto);
        return await Task.FromResult(StatusCode(201, result));
    }

    [HttpPost("evacuation/plan")]
    public async Task<ActionResult> CreatePlan()
    {
        var result = await service.CreateEvacPlans();
        return await Task.FromResult(StatusCode(201, result));
    }

    [HttpPut("evacuation/update")]
    public async Task<ActionResult<List<EvacuationZoneStatusDto>>> Update()
    {
        var result = await service.UpdateEvacPlans();
        if (result.Count <= 0)
            return NotFound(StatusCode(400, "Don't have any evacuation plans, pls create plans"));

        return await Task.FromResult(StatusCode(201, result));
    }

    [HttpDelete("evacuation/clear")]
    public async Task<ActionResult> Delete()
    {
        return await service.DeleteEvacPlans()
            ? Ok(StatusCode(200))
            : NotFound(StatusCode(404, "Evacuation plans list is empty"));
    }
}