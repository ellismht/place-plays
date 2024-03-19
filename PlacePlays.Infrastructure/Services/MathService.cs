using PlacePlays.Application.Models.Math;

namespace PlacePlays.Infrastructure.Services;

public class MathService : IMathService
{
    private const int EarthRadius = 6371000;
    private const double ToRadianValue = Math.PI / 180;

    public double GetDistanceBetweenTwoPoints(Point point1, Point point2)
    {
        var (lat1, lon1) = point1;
        var (lat2, lon2) = point2;
        
        var phi1 = ToRadian(lat1);
        var phi2 = ToRadian(lat2);

        var deltaPhi = Math.Sin(ToRadian(lat2 - lat1)/2);
        var deltaLambda = Math.Sin(ToRadian(lon2 - lon1)/2);

        var a = Math.Pow(deltaPhi, 2) + Math.Cos(phi1) * Math.Cos(phi2) * Math.Pow(deltaLambda, 2);
        
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return EarthRadius * c;
    }

    private static double ToRadian(double value)
    {
        return value * ToRadianValue;
    }
}