namespace PlacePlays.WebApi.Endpoints;

public static class SpotifyEndpoints
{
    public static RouteGroupBuilder MapSpotifyEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("TestGetInfo", TestGetInfo);
        
        group.MapGet("GetAuth", GetAuth);
        
        return group;
    }

    public static async Task<IResult> TestGetInfo()
    {

        return TypedResults.Ok();
    }
    
    public static async Task<IResult> GetAuth()
    {

        return TypedResults.Ok();
    }
}