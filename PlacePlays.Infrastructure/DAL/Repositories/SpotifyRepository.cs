using MongoDB.Driver;
using PlacePlays.Application.Abstractions;
using PlacePlays.Application.Services.Math;
using PlacePlays.Domain.Entities;
using PlacePlays.Infrastructure.Mapper;
using PlacePlays.Infrastructure.Models;

namespace PlacePlays.Infrastructure.DAL.Repositories;

internal class SpotifyRepository : IRepository
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
        var smth = await _context.SpotifyCollection.FindAsync(filterEmpty);
        
        var result = smth.ToEnumerable().Where(x =>
            calculator.GetDistanceBetweenTwoPoints(x.Latitude, x.Longitude) <= tracksInAreaSettings.Radius).ToList();

        return result.Select(x=> x.Map());
    }
}
