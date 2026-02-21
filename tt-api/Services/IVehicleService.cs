using tt_api.Dtos;
using tt_api.Models;

namespace tt_api.Services;

public interface IVehicleService
{
    Task<List<VehicalDto>> GetVehicals();
    Task<List<VehicleModel>>  CreateVehicles(List<CreateVehicleDto> dto);
}