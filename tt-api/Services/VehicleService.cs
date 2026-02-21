using tt_api.Dtos;
using tt_api.Models;

namespace tt_api.Services;

public class VehicleService(EvacuationStateService state) : IVehicleService
{
    public async Task<List<VehicalDto>> GetVehicals()
    {
        var vehicalDtos = state.VehicalDatas.Select(vehical => new VehicalDto()
        {
            VehicalID = vehical.VehicleID,
            Capacity = vehical.Capacity,
            Type = vehical.Type,
            Location = vehical.Location,
            Speed = vehical.Speed
        }).ToList();

        return vehicalDtos;
    }

    public async Task<List<VehicleModel>> CreateVehicles(List<CreateVehicleDto> dtos)
    {
        var newVehicles = dtos.Select(v => new VehicleModel
        {
            VehicleID = v.VehicalID,
            Capacity = v.Capacity,
            Type = v.Type,
            Location = v.Location,
            Speed = v.Speed
        }).ToList();

        foreach (var vehicle in newVehicles)
        {
            state.VehicalDatas.Add(vehicle);
        }

        return state.VehicalDatas;
    }
}