using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using PlacePlays.Mobile.Models.OptionModels;

namespace PlacePlays.Mobile;

public partial class App : Application
{
    public static HttpClient SpotifyAuth { get; private set; }
    public static HttpClient SpotifyClient { get; private set; }

    public App(IOptions<AuthOptionModel> authOptions, IOptions<SpotifyOptionModel> spotifyOptions)
    {
        InitializeComponent();

        MainPage = new AppShell();
        
        SpotifyAuth = new HttpClient()
        {
            BaseAddress = new Uri(authOptions.Value.BaseAddress),
            DefaultRequestHeaders = 
            { 
                Authorization = new AuthenticationHeaderValue(
                "Basic", 
                AuthExtensions.GetBase64String(authOptions.Value.ClientId, authOptions.Value.ClientSecret))
            }
        };

        SpotifyClient = new HttpClient()
        {
            BaseAddress = new Uri(spotifyOptions.Value.BaseAddress)
        };
    }
}