using PlacePlays.Domain.Entities;

namespace PlacePlays.Application.Services.Math;

public class DistanceCalculator
{
    private const int EarthRadius = 6371000; //in meters
    private const double ToRadianValue = System.Math.PI / 180;
    private readonly double _userCosLatRadian;
    private readonly Point _userLocation;

    public DistanceCalculator(in Point userLocation)
    {
        _userLocation = userLocation;
        var userLatRadian = _userLocation.Lat * ToRadianValue;
        _userCosLatRadian = System.Math.Cos(userLatRadian);
    }

    public double GetDistanceBetweenTwoPoints(in Point trackInfoPoint)
    {
        var phi2 = trackInfoPoint.Lat * ToRadianValue;

        var deltaPhi = System.Math.Sin((trackInfoPoint.Lat - _userLocation.Lat) * ToRadianValue/2);
        var deltaLambda = System.Math.Sin((trackInfoPoint.Lon - _userLocation.Lon) * ToRadianValue/2);

        var a = System.Math.Pow(deltaPhi, 2) + _userCosLatRadian * System.Math.Cos(phi2) * System.Math.Pow(deltaLambda, 2);
        
        var c = 2 * System.Math.Atan2(System.Math.Sqrt(a), System.Math.Sqrt(1 - a));

        return EarthRadius * c;
    }
}