using MongoDB.Driver;
using PlacePlays.WebApi.Entities;
using PlacePlays.WebApi.Services.Math;

namespace PlacePlays.WebApi.DAL;

internal class SpotifyRepository 
{
    private readonly MongoDbContext _context;
    public SpotifyRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async ValueTask InsertTrackInfo(SpotifyTrackInfo trackInfo)
    {
        var item = trackInfo.Map();
        await _context.SpotifyCollection.InsertOneAsync(item);
    }

    public async ValueTask<IEnumerable<SpotifyTrackInfo>> GetTracksInArea(SpotifyTracksInAreaSettings tracksInAreaSettings)
    {
        var userLocationPoint = new Point { Lat = tracksInAreaSettings.Lat, Lon = tracksInAreaSettings.Lon};
        var calculator = new DistanceCalculator(userLocationPoint);

        var filterEmpty = Builders<SpotifyEntity>.Filter.Empty;
        var collection = (await _context.SpotifyCollection.FindAsync(filterEmpty)).ToEnumerable();

        return collection.Where(x =>
            calculator.GetDistanceBetweenTwoPoints(x.Latitude, x.Longitude) <= tracksInAreaSettings.Radius)
            .Select(x=> x.Map()).ToList();
    }
}
