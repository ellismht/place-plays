using LiteDB;

namespace PlacePlays.Infrastructure.DAL.LiteDB;

public abstract class BaseLiteDbContext<T> : IDisposable
{
    public ILiteCollection<T> Collection { get; private set; }
    private LiteDatabase _db;
    private bool _isDisposed = true;
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    private void Dispose(bool disposing)
    {
        if (_isDisposed) return;
        if (disposing)
        {
            _db?.Dispose();
            _db = null;
        }

        _isDisposed = true;
    }
    
    protected void Create(string liteDbLocation, string collectionName)
    {
        if (_db is not null) return;
        
        _db = new LiteDatabase($"Filename={liteDbLocation}; Connection=Direct;");
        Collection = _db.GetCollection<T>(collectionName);
        _isDisposed = false;
    }
    
    ~BaseLiteDbContext() => Dispose(false);
}