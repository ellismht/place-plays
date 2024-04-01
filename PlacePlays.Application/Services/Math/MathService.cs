using PlacePlays.Application.Models.Math;

namespace PlacePlays.Application.Services.Math;

public class MathService : IMathService
{
    private const int EarthRadius = 6371000;
    private const double ToRadianValue = System.Math.PI / 180;

    public double GetDistanceBetweenTwoPoints(Point point1, Point point2)
    {
        var (lat1, lon1) = point1;
        var (lat2, lon2) = point2;
        
        var phi1 = ToRadian(lat1);
        var phi2 = ToRadian(lat2);

        var deltaPhi = System.Math.Sin(ToRadian(lat2 - lat1)/2);
        var deltaLambda = System.Math.Sin(ToRadian(lon2 - lon1)/2);

        var a = System.Math.Pow(deltaPhi, 2) + System.Math.Cos(phi1) * System.Math.Cos(phi2) * System.Math.Pow(deltaLambda, 2);
        
        var c = 2 * System.Math.Atan2(System.Math.Sqrt(a), System.Math.Sqrt(1 - a));

        return EarthRadius * c;
    }

    private static double ToRadian(double value)
    {
        return value * ToRadianValue;
    }
}