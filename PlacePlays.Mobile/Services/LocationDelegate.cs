using System.Diagnostics;
using PlacePlays.Mobile.Models.Spotify;
using PlacePlays.Mobile.Services.Api;
using PlacePlays.Mobile.Services.Spotify;
using Shiny;
using Shiny.Locations;
namespace PlacePlays.Mobile.Services;

public partial class LocationDelegate : IGpsDelegate
{
    private const int DelayMs = 150000;
    private readonly IClientService _client;
    private readonly IApiService _apiService;
    private string _trackId;

    public LocationDelegate(IClientService client, IApiService apiService)
    {
        _client = client;
        _apiService = apiService;
    }

    public async Task OnReading(GpsReading reading)
    {
        var track = await _client.GetCurrentlyPlayingTrack();
        var sw = Stopwatch.StartNew();

        if (track.Item is null)
        {
            sw.Stop();
            await Task.Delay(DelayMs);
            return;
        }

        if(_trackId is null)
            _trackId = track.Item.Id;
        else if (_trackId != track.Item.Id)
            _trackId = track.Item.Id;
        else
        {
            sw.Stop();
            await Task.Delay((int)(track.TrackRemainingMs + sw.ElapsedMilliseconds));
            return;
        }
        
        var record = new SpotifyRecord(_trackId, reading.Position.Latitude, reading.Position.Longitude, TimeProvider.System.GetLocalNow());
        await _apiService.PostSpotifyRecord(record);

        sw.Stop();
        await Task.Delay((int)(track.TrackRemainingMs + sw.ElapsedMilliseconds));
    }
}

#if ANDROID
public partial class LocationDelegate : IAndroidForegroundServiceDelegate
{
    public void Configure(AndroidX.Core.App.NotificationCompat.Builder builder)
    {
        builder.SetContentTitle("PlacePlays")
            .SetContentText("Sprawdzamy czego słuchasz")
            .Build();
    }
}
#endif