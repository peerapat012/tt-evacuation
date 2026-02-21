using tt_api.Dtos;
using tt_api.Models;

namespace tt_api.Datas;

public class EvacuationZoneMockupData
{
    public static List<EvacuationZoneModel> EvacuationZones =
    [
        new EvacuationZoneModel
        {
            ZoneID = "Z1",
            LocationCoordinates = new LocationCoordinates { Latitude = 13.7563, Longitude = 100.5018 },
            NumberOfPeople = 100,
            UrgencyLevel = 4,
            RemainingPeople = 100,
            TotalEvacuated = 0,
        },
        new EvacuationZoneModel
        {
            ZoneID = "Z2",
            LocationCoordinates = new LocationCoordinates { Latitude = 13.7367, Longitude = 100.5231 },
            NumberOfPeople = 50,
            UrgencyLevel = 5,
            RemainingPeople = 50,
            TotalEvacuated = 0,
        }
    ];
}