using System.Collections.Immutable;
using MongoDB.Driver;
using PlacePlays.WebApi.DAL;
using PlacePlays.WebApi.Entities;
using PlacePlays.WebApi.Models;
using PlacePlays.WebApi.Services.Math;

namespace PlacePlays.WebApi.Endpoints.Tracks;

public class GetTracksInArea : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) => app
        .MapGet("getTracksInArea", Handle)
        .Produces<ImmutableList<Response>>();
    
    public record Request(double Lat, double Lon, int Radius);
    
    public record Response(string Id, string TrackId, double Latitude, double Longitude, DateTime Date);

    private static async Task<IResult> Handle([AsParameters]Request request, MongoDbContext context)
    {
        var userLocationPoint = new Point { Lat = request.Lat, Lon = request.Lon};
        var calculator = new DistanceCalculator(userLocationPoint);

        var filterEmpty = Builders<SpotifyEntity>.Filter.Empty;
        var collection = (await context.SpotifyCollection.FindAsync(filterEmpty)).ToEnumerable();

        var result = collection.Where(x =>
                calculator.GetDistanceBetweenTwoPoints(x.Latitude, x.Longitude) <= request.Radius)
            .Select(x=> new Response(x.Id, x.TrackId, x.Latitude, x.Longitude, x.Date)).ToImmutableList();
        
        return TypedResults.Ok(result);
    }
}