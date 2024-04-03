using MongoDB.Driver;
using PlacePlays.Application.Abstractions;
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

    public async ValueTask<IEnumerable<SpotifyTrackInfo>> GetTracksInArea()
    {
        //return (await _context.SpotifyCollection.FindAsync(entity => entity.TrackId == "string"))
        return new List<SpotifyTrackInfo>();
    }
}
