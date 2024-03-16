using LiteDB;
using Microsoft.Extensions.Options;
using PlacePlays.Application.Models;
using PlacePlays.Application.Models.Options;

namespace PlacePlays.Infrastructure.DAL.LiteDB;

public class SpotifyLiteDbContext : IDisposable
{
    public ILiteCollection<SpotifyLiteDbModel> Collection { get; private set; } = null;
    private readonly LiteDbOptions _options;
    private LiteDatabase _db = null;
    private const string CollectionName = "Spotify";
    public SpotifyLiteDbContext(IOptions<LiteDbOptions> options)
    {
        _options = options.Value;
        Create();
    }

    private void Create()
    {
        if (_db is not null) return;
        
        var localization = $@"\\{Environment.MachineName}" + _options.SpotifyDbLocation;
        _db = new LiteDatabase($"Filename={localization}; Connection=Direct;");
        Collection = _db.GetCollection<SpotifyLiteDbModel>(CollectionName);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}