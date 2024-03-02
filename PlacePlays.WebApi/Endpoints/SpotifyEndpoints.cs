namespace PlacePlays.WebApi.Endpoints;

public static class SpotifyEndpoints
{
    public static RouteGroupBuilder MapSpotifyEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("TestGetInfo", TestGetInfo);
        
        return group;
    }

    public static async Task<IResult> TestGetInfo()
    {

        return TypedResults.Ok();
    }
}