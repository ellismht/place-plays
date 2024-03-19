using PlacePlays.Application.Models.Math;

namespace PlacePlays.Infrastructure.Services;

public interface IMathService
{
    double GetDistanceBetweenTwoPoints(Point point1, Point point2);
}