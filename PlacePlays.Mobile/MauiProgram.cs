using System.Net.Http.Headers;
using System.Reflection;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PlacePlays.Mobile.Models.OptionModels;
using PlacePlays.Mobile.Pages;

namespace PlacePlays.Mobile;

public static class MauiProgram
{
    private const string AuthSectionName = "Auth"; 
    public static HttpClient SpotifyAuth;
    public static AuthOptionModel AuthOptions;
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
        
        AuthOptions = builder.Configuration.GetOptions<AuthOptionModel>(AuthSectionName);

        SpotifyAuth = new HttpClient()
        {
            BaseAddress = new Uri(AuthOptions.BaseAuthAddress)
        };
        SpotifyAuth.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic", 
            AuthExtensions.GetBase64String(AuthOptions.ClientId, AuthOptions.ClientSecret));

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<AuthPage>();

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