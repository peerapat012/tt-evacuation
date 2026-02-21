using tt_api.Dtos;
using tt_api.Models;
using tt_api.Utils;

namespace tt_api.Services;

public class EvacuationService(EvacuationStateService state) : IEvacuationService
{
    public async Task<List<EvacuationZoneStatusDto>> GetEvacZoneStatus()
    {
        var evacStatusDto = state.ZoneDatas.Select(zone => new EvacuationZoneStatusDto
        {
            ZoneID = zone.ZoneID,
            TotalEvacuated = zone.TotalEvacuated,
            RemainingPeople = zone.RemainingPeople,
            LastVehicleUsed = zone.LastVehicleUsed,
        }).ToList();

        return evacStatusDto;
    }

    public async Task<List<EvacuationZoneModel>> CreateEvacuationZone(List<CreateEvacuationZoneDto> dtos)
    {
        var newZones = dtos.Select(zone => new EvacuationZoneModel
        {
            ZoneID = zone.ZoneID,
            NumberOfPeople = zone.NumberOfPeople,
            UrgencyLevel = zone.UrgencyLevel,
            LocationCoordinates = zone.LocationCoordinates,
            TotalEvacuated = 0,
            RemainingPeople = zone.NumberOfPeople,
        }).ToList();

        foreach (var zone in newZones)
        {
            state.ZoneDatas.Add(zone);
        }

        return state.ZoneDatas;
    }

    public async Task<List<EvacuationPlanDto>> CreateEvacPlans()
    {
        state.Plans.Clear();
        var vehicleData = state.VehicalDatas.ToHashSet();
        var orderZones = state.ZoneDatas.OrderByDescending(x => x.UrgencyLevel);
        foreach (var zone in orderZones)
        {
            var bestVehicleForZone = vehicleData.Select(v => new
                {
                    Vehicle = v,
                    Distance = EvacautionUtil.CalculateDistance(zone.LocationCoordinates, v.Location),
                    evacMax = Math.Max(zone.RemainingPeople, v.Capacity)
                })
                .OrderByDescending(x => x.evacMax)
                .ThenBy(x => x.Distance)
                .FirstOrDefault();
            if (bestVehicleForZone == null)
            {
                state.Plans.Add(new EvacuationPlanDto
                {
                    ZoneID = zone.ZoneID,
                    VehicalID = "No available vehicle",
                    ETA = "",
                    NumberOfPeople = zone.NumberOfPeople
                });
                continue;
            }

            ;

            var eta = EvacautionUtil.CalculateEta(bestVehicleForZone.Distance, bestVehicleForZone.Vehicle.Speed);

            zone.LastVehicleUsed = bestVehicleForZone.Vehicle.VehicalID;

            state.Plans.Add(new EvacuationPlanDto
            {
                ZoneID = zone.ZoneID,
                VehicalID = bestVehicleForZone.Vehicle.VehicalID,
                ETA = (int)eta.TotalMinutes > 0
                    ? $"{(int)eta.TotalMinutes} Minutes"
                    : $"{(int)eta.TotalSeconds} Seconds",
                NumberOfPeople = zone.NumberOfPeople
            });

            vehicleData.Remove(bestVehicleForZone.Vehicle);
        }

        return state.Plans;
    }

    public async Task<List<EvacuationZoneStatusDto>> UpdateEvacPlans()
    {
        if (state.Plans.Count <= 0)
            return [];

        foreach (var zoneData in state.ZoneDatas)
        {
            if (zoneData.RemainingPeople <= 0) continue;

            var vehicleData = state.VehicalDatas.FirstOrDefault(v => v.VehicalID == zoneData.LastVehicleUsed);
            if (vehicleData == null) continue;
            var minEvac = Math.Min(zoneData.RemainingPeople, vehicleData.Capacity);
            zoneData.TotalEvacuated += minEvac;
            var remainingPeople = zoneData.NumberOfPeople - zoneData.TotalEvacuated;
            zoneData.RemainingPeople = remainingPeople <= 0 ? 0 : remainingPeople;
        }

        var zoneStatus = state.ZoneDatas.Select(z => new EvacuationZoneStatusDto
        {
            ZoneID = z.ZoneID,
            RemainingPeople = z.RemainingPeople,
            LastVehicleUsed = z.LastVehicleUsed,
            TotalEvacuated = z.TotalEvacuated
        }).ToList();

        return zoneStatus;
    }

    public async Task<bool> DeleteEvacPlans()
    {
        if (state.Plans.Capacity <= 0) return false;
        state.Plans.Clear();
        return true;
    }
}