using System.Text.Json.Serialization;

namespace tt_api.Models;

public class VehicalModel
{
    public string VehicalID { get; set; }
    public int Capacity { get; set; }
    public string Type { get; set; }
    public LocationCoordinates Location { get; set; }
    public int Speed { get; set; }
}
