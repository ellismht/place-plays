using PlacePlays.Application.Services.Spotify;
using PlacePlays.WebApi.Mapper;
using PlacePlays.WebApi.Requests;

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
        await service.InsertTrackInfo(request.Map());
        
        return TypedResults.Created();
    }

    public static async Task<IResult> GetTracksInArea([AsParameters]GetTracksInAreaRequest request, ISpotifyService service)
    {
        var result = await service.GetTracksInArea(request.Map());
        
        return TypedResults.Ok(result);
    }
}