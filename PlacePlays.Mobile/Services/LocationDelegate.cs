using Microsoft.Extensions.Logging;
using Shiny;
using Shiny.Locations;
namespace PlacePlays.Mobile.Services;

public partial class LocationDelegate : GpsDelegate
{
    public LocationDelegate(ILogger logger) : base(logger)
    {
    }

    protected override Task OnGpsReading(GpsReading reading)
    {
        var smth = reading;
        
        return Task.CompletedTask;
    }
}

#if ANDROID
public partial class MyGpsDelegate : IAndroidForegroundServiceDelegate
{
    public void Configure(AndroidX.Core.App.NotificationCompat.Builder builder)
    {
        
    }
}
#endif