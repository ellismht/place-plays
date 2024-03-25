using PlacePlays.Application.Mappers;
using PlacePlays.Application.Models;
using PlacePlays.Infrastructure.DAL;

namespace PlacePlays.WebApi.Endpoints;

public static class SpotifyEndpoints
{
    public static RouteGroupBuilder MapSpotifyEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("saveTrackLocationInfo", SaveTrackLocationInfo);
        
        return group;
    }

    public static async Task<IResult> SaveTrackLocationInfo(SaveTrackLocationRequest body, ISpotifyRepository repository)
    {
        var spotifyItem = body.Map();

        await repository.AddTrackWithGps(spotifyItem);
        
        return TypedResults.Ok();
    }
}