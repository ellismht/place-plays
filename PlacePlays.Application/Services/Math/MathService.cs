using PlacePlays.Application.Models.Math;

namespace PlacePlays.Application.Services.Math;

public class MathService : IMathService
{
    private const int EarthRadius = 6371000; //in meters
    private const double ToRadianValue = System.Math.PI / 180;
    private readonly double _userCosLatRadian;

    public MathService(Point userLocation)
    {
        var userLatRadian = userLocation.Lat * ToRadianValue;
        _userCosLatRadian = System.Math.Cos(userLatRadian);
    }

    public double GetDistanceBetweenTwoPoints(Point userLocation, Point trackInfoPoint)
    {
        var (lat1, lon1) = userLocation;
        var (lat2, lon2) = trackInfoPoint;
        
        var phi2 = lat2 * ToRadianValue;

        var deltaPhi = System.Math.Sin((lat2 - lat1) * ToRadianValue/2);
        var deltaLambda = System.Math.Sin((lon2 - lon1) * ToRadianValue/2);

        var a = System.Math.Pow(deltaPhi, 2) + _userCosLatRadian * System.Math.Cos(phi2) * System.Math.Pow(deltaLambda, 2);
        
        var c = 2 * System.Math.Atan2(System.Math.Sqrt(a), System.Math.Sqrt(1 - a));

        return EarthRadius * c;
    }
}