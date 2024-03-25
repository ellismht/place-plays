using PlacePlays.Application.Models;
using PlacePlays.Application.Models.LiteDb;

namespace PlacePlays.Application.Mappers;

public static class RequestMapper
{
    public static SpotifyLiteDbModel Map(this SaveTrackLocationRequest request)
    {
        return new SpotifyLiteDbModel
        {
            Id = request.Id,
            Longitude = request.Lon,
            Latitude = request.Lat,
            Date = request.AddDate
        };
    }
}