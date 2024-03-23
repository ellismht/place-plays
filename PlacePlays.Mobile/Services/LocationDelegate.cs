using PlacePlays.Mobile.Models.Spotify;
using PlacePlays.Mobile.Services.Spotify;
using Shiny;
using Shiny.Locations;
namespace PlacePlays.Mobile.Services;

public partial class LocationDelegate : IGpsDelegate
{
    private readonly IClientService _client;
    private string _trackId;

    public LocationDelegate(IClientService client)
    {
        _client = client;
    }

    public async Task OnReading(GpsReading reading)
    {
        var track = await _client.GetCurrentlyPlayingTrack();

        if (track.Item is null)
        {
            await Task.Delay(150000);
            return;
        }

        if(_trackId is null)
            _trackId = track.Item.Id;
        else if (_trackId != track.Item.Id)
            _trackId = track.Item.Id;
        else
        {
            await Task.Delay(track.TrackRemainingMs + 5000);
            return;
        }

        var record = new SpotifyRecord(_trackId, reading.Position.Latitude, reading.Position.Longitude, TimeProvider.System.GetLocalNow());
        
        

    }
}

#if ANDROID
public partial class LocationDelegate : IAndroidForegroundServiceDelegate
{
    public void Configure(AndroidX.Core.App.NotificationCompat.Builder builder)
    {
        builder.SetContentTitle("PlacePlays")
            .SetContentText("Sprawdzamy czego słuchasz")
            .SetSmallIcon(Microsoft.Maui.Resource.Mipmap.appicon)
            .Build();
    }
}
#endif