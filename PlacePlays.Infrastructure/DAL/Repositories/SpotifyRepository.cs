using LiteDB;
using PlacePlays.Application.Models;
using PlacePlays.Infrastructure.DAL.LiteDB;

namespace PlacePlays.Infrastructure.DAL.Repositories;

public class SpotifyRepository : ISpotifyRepository
{
    private readonly ILiteCollection<SpotifyLiteDbModel> _collection;
    public SpotifyRepository(SpotifyLiteDbContext context)
    {
        _collection = context.Collection;
    }

    public ValueTask AddTrackWithGps(SpotifyLiteDbModel item)
    {
        _collection.Insert(item);

        return ValueTask.CompletedTask;
    }
    
    public void GetTestInfo()
    {
        //_spotifyClient.GetAsync();
    }
}