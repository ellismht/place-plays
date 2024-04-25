using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PlacePlays.WebApi.Entities;
using PlacePlays.WebApi.Models;
using PlacePlays.WebApi.Models.Options;

namespace PlacePlays.WebApi.DAL;

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

