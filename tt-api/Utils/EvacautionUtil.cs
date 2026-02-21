using tt_api.Models;

namespace tt_api.Utils;

public class EvacautionUtil
{
    public static double CalculateDistance(LocationCoordinates zoneLocation, LocationCoordinates vehicleLocation)
    {
        const double EarthRadius = 6371;

        var deltaLatitude = ToRadians(vehicleLocation.Latitude - zoneLocation.Latitude);
        var deltaLongtitude = ToRadians(vehicleLocation.Longitude - zoneLocation.Longitude);
        var rLat1 = ToRadians(zoneLocation.Latitude);
        var rLat2 = ToRadians(vehicleLocation.Latitude);

        var a = Math.Pow(Math.Sin(deltaLatitude / 2), 2) +
                Math.Pow(Math.Sin(deltaLongtitude / 2), 2) *
                Math.Cos(rLat1) *
                Math.Cos(rLat2);
        var c = 2 * Math.Asin(Math.Sqrt(a));

        return EarthRadius * c;
    }

    public static TimeSpan CalculateEta(double distance, int speed)
    {
        double hours = distance / speed;

        return TimeSpan.FromHours(hours);
    }

    private static double ToRadians(double angle)
    {
        return Math.PI / 180 * angle;
    }
}