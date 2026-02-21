using tt_api.Datas;
using tt_api.Dtos;
using tt_api.Models;

namespace tt_api.Services;

public class EvacuationStateService
{
    public List<EvacuationPlanDto> Plans { get; set; } = new();
    public List<EvacuationZoneModel> ZoneDatas { get; set; } = EvacuationZoneMockupData.EvacuationZones;
    public List<VehicalModel> VehicalDatas { get; set; } = VehicalMockupData.Vehicals;
}