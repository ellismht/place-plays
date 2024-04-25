using PlacePlays.WebApi.DAL;
using PlacePlays.WebApi.Entities;

namespace PlacePlays.WebApi.Endpoints.Tracks;

public class InsertTrackLocationInfo : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) => app
        .MapPost("insertTrackLocationInfo", Handle);
    
    public record Request(string Id, double Lat, double Lon, DateTime AddDate);

    private static async Task<IResult> Handle(Request request, MongoDbContext context)
    {
        var trackInfo = new SpotifyEntity
            {
                TrackId = request.Id, 
                Latitude = request.Lat, 
                Longitude = request.Lon, 
                Date = request.AddDate
            };
        
        await context.SpotifyCollection.InsertOneAsync(trackInfo);
        
        return TypedResults.Created();
    }
}