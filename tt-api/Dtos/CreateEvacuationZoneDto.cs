using tt_api.Models;

namespace tt_api.Dtos;

public class CreateEvacuationZoneDto
{
    public string ZoneID { get; set; } = string.Empty;
    public LocationCoordinates LocationCoordinates { get; set; } = new LocationCoordinates();
    public int NumberOfPeople { get; set; }
    public int UrgencyLevel { get; set; }
} 