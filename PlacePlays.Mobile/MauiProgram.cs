using CommunityToolkit.Maui;
using IdentityModel.OidcClient;
using Microsoft.Extensions.Logging;

namespace PlacePlays.Mobile;

public static class MauiProgram
{
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

        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddSingleton(new OidcClient((new()
        {
            Authority = "https://accounts.spotify.com/",

            Scope = "user-read-private user-read-email",
            RedirectUri = "placePlays://auth",
            StateLength = 16,
            Browser = new AuthBrowser()
        })));

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}