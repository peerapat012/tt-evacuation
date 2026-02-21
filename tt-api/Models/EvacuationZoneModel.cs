namespace tt_api.Models;

public class EvacuationZoneModel
{
    public string ZoneID { get; set; } = string.Empty;
    public LocationCoordinates LocationCoordinates { get; set; } = new LocationCoordinates();
    public int NumberOfPeople { get; set; }
    public int UrgencyLevel { get; set; }
    public int TotalEvacuated { get; set; } = 0;
    public int RemainingPeople { get; set; } = 0;
    public string? LastVehicleUsed { get; set; } = string.Empty;
}

public class LocationCoordinates
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}