using PlacePlays.WebApi.Models;

namespace PlacePlays.WebApi.Services.Math;

public class DistanceCalculator
{
    private const int EarthRadius = 6371; //in km
    private const double ToRadianValue = System.Math.PI / 180;
    private readonly double _userCosLatRadian;
    private readonly Point _userLocation;

    public DistanceCalculator(in Point userLocation)
    {
        _userLocation = userLocation;
        var userLatRadian = _userLocation.Lat * ToRadianValue;
        _userCosLatRadian = System.Math.Cos(userLatRadian);
    }

    public double GetDistanceBetweenTwoPoints(in double trackInfoLat, in double trackInfoLon)
    {
        var phi2 = trackInfoLat * ToRadianValue;

        var deltaPhi = System.Math.Sin((trackInfoLat - _userLocation.Lat) * ToRadianValue/2);
        var deltaLambda = System.Math.Sin((trackInfoLon - _userLocation.Lon) * ToRadianValue/2);

        var a = System.Math.Pow(deltaPhi, 2) + _userCosLatRadian * System.Math.Cos(phi2) * System.Math.Pow(deltaLambda, 2);
        
        var c = 2 * System.Math.Atan2(System.Math.Sqrt(a), System.Math.Sqrt(1 - a));

        return EarthRadius * c;
    }
}