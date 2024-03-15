using LiteDB;
using Microsoft.Extensions.Options;
using PlacePlays.Application.Models.Options;

namespace PlacePlays.Infrastructure.DAL.LiteDB;

public class SpotifyLiteDBContext : IDisposable
{
    private LiteDatabase _db;
    public SpotifyLiteDBContext(IOptions<LiteDbOptions> options)
    {
        
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}