using System.Net.Http.Headers;
using System.Reflection;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PlacePlays.Mobile.Models;

namespace PlacePlays.Mobile;

public static class MauiProgram
{
    public static HttpClient SpotifyAuth;
    public static TokenModel TokenData;
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        var ass = Assembly.GetExecutingAssembly();
        using var stream = ass.GetManifestResourceStream("PlacePlays.Mobile.appsettings.json");
        var config = new ConfigurationBuilder().AddJsonStream(stream).Build();

        builder.Configuration.AddConfiguration(config);

        SpotifyAuth = new HttpClient()
        {
            BaseAddress = new Uri("https://accounts.spotify.com")
        };
        
        SpotifyAuth.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic", 
            AuthExtensions.GetBase64String(config["Auth:ClientId"], config["Auth:ClientSecret"]));

        builder.Services.AddSingleton<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}