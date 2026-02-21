using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using tt_api.Dtos;
using tt_api.Services;
using tt_api.Services.Caching;
using tt_api.Utils;

namespace tt_api.Controllers;

[Route("api/")]
[ApiController]
public class ZoneController(
    EvacuationStateService stateData,
    IEvacuationService service,
    IRedisService redisService,
    ILogger<Program> logger)
    : ControllerBase
{
    [HttpGet("evacuation/status")]
    public async Task<ActionResult<List<EvacuationZoneStatusDto>>> Get()
    {
        var evacuationZoneStatus = redisService.GetData<IEnumerable<EvacuationZoneStatusDto>>("zone_status");
        if (evacuationZoneStatus == null)
        {
            evacuationZoneStatus = await service.GetEvacZoneStatus();
            redisService.SetData("zone_status", evacuationZoneStatus);
        }

        //logging
        foreach (var zone in evacuationZoneStatus)
        {
            logger.LogInformation(
                "Zone ID: {id}, Total Evacuated: {totalEvac}, Remaining People: {people}, Last Vehicle Used: {vehicleId} ",
                zone.ZoneID,
                zone.TotalEvacuated,
                zone.RemainingPeople,
                zone.LastVehicleUsed);
        }

        return await Task.FromResult(StatusCode(200, evacuationZoneStatus));
    }

    [HttpPost("evacuation-zones")]
    public async Task<ActionResult> CreateEvacuationZones([FromBody] List<CreateEvacuationZoneDto> evacZonesDto)
    {
        if (stateData.ZoneDatas.Select(z => z.ZoneID)
            .Any(id => evacZonesDto.Select(ev => ev.ZoneID.ToLower()).Contains(id.ToLower())))
        {
            return BadRequest("Zone ID is already in use");
        }

        var result = await service.CreateEvacuationZone(evacZonesDto);
        var zoneStatus = await service.GetEvacZoneStatus();
        redisService.SetData("zone_status", zoneStatus);
        return await Task.FromResult(StatusCode(201, result));
    }

    [HttpPost("evacuation/plan")]
    public async Task<ActionResult> CreatePlan()
    {
        var result = await service.CreateEvacPlans();

        //logging
        logger.LogInformation("Plan: {plan} created", result.Count);
        foreach (var (plan, i) in result.Select((v, i) => (v, i)))
        {
            logger.LogInformation(
                "Plan: {index}, Zone ID: {zoneId}, Vehical ID: {vehId}, ETA: {eta}, Number of people to be evacuated: {people}",
                i + 1,
                plan.ZoneID,
                plan.VehicalID,
                plan.ETA,
                plan.NumberOfPeople);
        }

        return await Task.FromResult(StatusCode(201, result));
    }

    [HttpPut("evacuation/update")]
    public async Task<ActionResult<List<EvacuationZoneStatusDto>>> Update()
    {
        var result = await service.UpdateEvacPlans();

        if (result.Count <= 0)
            return NotFound(StatusCode(400, "Don't have any evacuation plans, pls create plans"));
        redisService.SetData("zone_status", result);

        //logging
        foreach (var zone in result)
        {
            logger.LogInformation(
                "Zone ID: {id}, Total Evacuated: {totalEvac}, Remaining People: {people}, Last Vehicle Used: {vehicleId} ",
                zone.ZoneID,
                zone.TotalEvacuated,
                zone.RemainingPeople,
                zone.LastVehicleUsed);
        }

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