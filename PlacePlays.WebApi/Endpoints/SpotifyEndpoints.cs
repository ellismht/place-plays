using PlacePlays.Application.Services.Spotify;
using PlacePlays.WebApi.Mapper;
using PlacePlays.WebApi.Requests;

namespace PlacePlays.WebApi.Endpoints;

public static class SpotifyEndpoints
{
    public static RouteGroupBuilder MapSpotifyEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("saveTrackLocationInfo", SaveTrackLocationInfo);
        
        return group;
    }

    public static async Task<IResult> SaveTrackLocationInfo(SaveTrackLocationRequest body, ISpotifyService service)
    {
        await service.InsertTrackInfo(body.Map());
        
        return TypedResults.Created();
    }

    public static async Task<IResult> GetTracksInArea(GetTracksInAreaRequest body, ISpotifyService service)
    {
        var result = await service.GetTracksInArea();
        
        return TypedResults.Ok(result);
    }
}