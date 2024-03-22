using Shiny;
using Shiny.Locations;
namespace PlacePlays.Mobile.Services;

public partial class LocationDelegate : IGpsDelegate
{
    public LocationDelegate()
    {
    }

    public Task OnReading(GpsReading reading)
    {
        var smth = reading;
        
        return Task.CompletedTask;
    }
}

#if ANDROID
public partial class LocationDelegate : IAndroidForegroundServiceDelegate
{
    public void Configure(AndroidX.Core.App.NotificationCompat.Builder builder)
    {
        builder.SetContentTitle("PlacePlays").SetContentText("Sprawdzamy czego słuchasz").Build();
    }
}
#endif