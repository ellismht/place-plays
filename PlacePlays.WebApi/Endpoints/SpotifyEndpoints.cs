using PlacePlays.WebApi.Entities;
using PlacePlays.WebApi.Requests;
using PlacePlays.WebApi.Services.Spotify;

namespace PlacePlays.WebApi.Endpoints;

public static class SpotifyEndpoints
{
    public static RouteGroupBuilder MapSpotifyEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("saveTrackLocationInfo", SaveTrackLocationInfo);

        group.MapGet("getTracksInArea", GetTracksInArea);
        
        return group;
    }

    public static async Task<IResult> SaveTrackLocationInfo(SaveTrackLocationRequest request, ISpotifyService service)
    {
        var trackInfo = new SpotifyTrackInfo(null, request.Id, request.Lat, request.Lon, request.AddDate);
        await service.InsertTrackInfo(trackInfo);
        
        return TypedResults.Created();
    }

    public static async Task<IResult> GetTracksInArea([AsParameters]GetTracksInAreaRequest request, ISpotifyService service)
    {
        var tracksInAreaSettings = new SpotifyTracksInAreaSettings(request.Lat, request.Lon, request.Radius);
        var result = await service.GetTracksInArea(tracksInAreaSettings);
        
        return TypedResults.Ok(result);
    }
}