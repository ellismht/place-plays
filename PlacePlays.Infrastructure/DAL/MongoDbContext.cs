using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PlacePlays.Application.Models.Options;
using PlacePlays.Infrastructure.Models;

namespace PlacePlays.Infrastructure.DAL;

public class MongoDbContext
{
    public readonly IMongoCollection<SpotifyEntity> SpotifyCollection;

    public MongoDbContext(IOptions<MongoDbOptions> options)
    {
        var client = new MongoClient(options.Value.ConnectionUri);
        var database = client.GetDatabase(options.Value.DatabaseName);
        SpotifyCollection = database.GetCollection<SpotifyEntity>(options.Value.CollectionName);   
    }
}

