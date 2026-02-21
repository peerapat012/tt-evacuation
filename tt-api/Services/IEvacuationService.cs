using tt_api.Dtos;
using tt_api.Models;

namespace tt_api.Services;

public interface IEvacuationService
{
    Task<List<EvacuationZoneStatusDto>> GetEvacZoneStatus();
    Task<List<EvacuationZoneModel>> CreateEvacuationZone(List<CreateEvacuationZoneDto> dtos);
    Task<List<EvacuationPlanDto>> CreateEvacPlans();
    Task<List<EvacuationZoneStatusDto>> UpdateEvacPlans();
    Task<bool> DeleteEvacPlans();
}