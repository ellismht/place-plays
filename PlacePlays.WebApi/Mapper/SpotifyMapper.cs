using PlacePlays.Domain.Entities;
using PlacePlays.WebApi.Requests;

namespace PlacePlays.WebApi.Mapper;

public static class SpotifyMapper
{
    public static SpotifyTrackInfo Map(this SaveTrackLocationRequest request)
    {
        return new SpotifyTrackInfo(null,request.Id, request.Lat, request.Lon, request.AddDate);
    }

    public static SpotifyTracksInAreaSettings Map(this GetTracksInAreaRequest request)
    {
        return new SpotifyTracksInAreaSettings(request.Lat, request.Lon, request.Radius);
    }
}