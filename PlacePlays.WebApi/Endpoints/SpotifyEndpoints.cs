using PlacePlays.Application.Models;

namespace PlacePlays.WebApi.Endpoints;

public static class SpotifyEndpoints
{
    public static RouteGroupBuilder MapSpotifyEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("saveTrackLocationInfo", SaveTrackLocationInfo);
        
        return group;
    }

    public static async Task<IResult> SaveTrackLocationInfo(SaveTrackLocationRequest body)
    {

        //await repository.AddTrackWithGps(spotifyItem);
        
        return TypedResults.Ok();
    }
}