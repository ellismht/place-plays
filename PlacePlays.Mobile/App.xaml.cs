using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using PlacePlays.Mobile.Models.OptionModels;

namespace PlacePlays.Mobile;

public partial class App : Application
{
    public static AuthOptionModel AuthOptions { get; private set; }
    public static HttpClient SpotifyAuth { get; private set; }

    public App(IOptions<AuthOptionModel> authOptions)
    {
        InitializeComponent();

        MainPage = new AppShell();
        AuthOptions = authOptions.Value;
        
        SpotifyAuth = new HttpClient()
        {
            BaseAddress = new Uri(authOptions.Value.BaseAuthAddress),
            DefaultRequestHeaders = 
            { 
                Authorization = new AuthenticationHeaderValue(
                "Basic", 
                AuthExtensions.GetBase64String(authOptions.Value.ClientId, authOptions.Value.ClientSecret))
            }
        };
    }
}