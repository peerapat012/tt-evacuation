using tt_api.Models;

namespace tt_api.Dtos;

public class CreateVehicleDto
{
    public string VehicalID { get; set; }
    public int Capacity { get; set; }
    public string Type { get; set; }
    public LocationCoordinates Location { get; set; }
    public int Speed { get; set; }
}