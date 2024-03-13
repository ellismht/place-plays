using System.Reflection;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PlacePlays.Mobile.Models.OptionModels;
using PlacePlays.Mobile.Pages;
using PlacePlays.Mobile.Services.Auth;
using PlacePlays.Mobile.Services.Spotify;
using PlacePlays.Mobile.ViewModels;

namespace PlacePlays.Mobile;

public static class MauiProgram
{
    private const string AuthSectionName = "Auth"; 
    private const string SpotifySectionName = "Spotify"; 
    
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

        builder.Services.Configure<AuthOptionModel>(builder.Configuration.GetSection(AuthSectionName));
        builder.Services.Configure<SpotifyOptionModel>(builder.Configuration.GetSection(SpotifySectionName));
        
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<AuthPage>();
        
        builder.Services.AddSingleton<MainViewModel>();

        builder.Services.AddSingleton<IAuthService, SpotifyAuthService>();
        builder.Services.AddSingleton<IClientService, SpotifyClientService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    private static T GetOptions<T>(this ConfigurationManager config, string sectionName) where T : class, new()
    {
        T options = new();
        var section = config.GetSection(sectionName);
        section.Bind(options);

        return options;
    }
}