namespace tt_api.Dtos;

public class EvacuationPlanDto
{
    public string ZoneID { get; set; } = string.Empty;
    public string VehicalID { get; set; } = string.Empty;
    public string ETA { get; set; } = string.Empty;
    public int NumberOfPeople { get; set; }
}