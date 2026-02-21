namespace tt_api.Dtos;

public class EvacuationZoneStatusDto
{
    public string ZoneID { get; set; } = string.Empty;
    public int TotalEvacuated { get; set; }
    public int RemainingPeople { get; set; }
    public string? LastVehicleUsed { get; set; } = string.Empty;
}