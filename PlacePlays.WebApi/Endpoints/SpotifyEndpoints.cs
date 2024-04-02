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
}