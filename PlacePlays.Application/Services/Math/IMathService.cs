using PlacePlays.Application.Models.Math;

namespace PlacePlays.Application.Services.Math;

public interface IMathService
{
    double GetDistanceBetweenTwoPoints(Point userPoint, Point trackInfoPoint);
}