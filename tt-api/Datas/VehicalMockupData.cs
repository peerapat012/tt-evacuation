using tt_api.Models;

namespace tt_api.Datas;

public class VehicalMockupData
{
    public static List<VehicalModel> Vehicals =
    [
        new VehicalModel
        {
            VehicalID = "V1",
            Capacity = 40,
            Type = "bus",
            Location = new LocationCoordinates { Latitude = 13.7650, Longitude = 100.5381 },
            Speed = 60
        },
        new VehicalModel
        {
            VehicalID = "V2",
            Capacity = 20,
            Type = "van",
            Location = new LocationCoordinates { Latitude = 13.7320, Longitude = 100.5200 },
            Speed = 50
        }
    ];
}