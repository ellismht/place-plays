using Microsoft.Extensions.Options;
using PlacePlays.Application.Models;
using PlacePlays.Application.Models.Options;

namespace PlacePlays.Infrastructure.DAL.LiteDB;

public class SpotifyLiteDbContext : BaseLiteDbContext<SpotifyLiteDbModel>
{
    private const string CollectionName = "Spotify";

    public SpotifyLiteDbContext(IOptions<LiteDbOptions> options)
    {
        Create(options.Value.SpotifyDbLocation, CollectionName);
    }
}