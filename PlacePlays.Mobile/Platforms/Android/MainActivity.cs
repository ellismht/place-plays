using System.Text.Json;
using Android.App;
using Android.Content;
using Android.Content.PM;
using PlacePlays.Mobile.Models;

namespace PlacePlays.Mobile;

[Activity(Theme = "@style/Maui.SplashTheme", LaunchMode = LaunchMode.SingleTop, MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
[IntentFilter([Intent.ActionView],
    DataScheme = "pp",
    DataHost = "auth",
    AutoVerify = true,
    Categories = [ Intent.ActionView, Intent.CategoryDefault, Intent.CategoryBrowsable ])]
public class MainActivity : MauiAppCompatActivity
{
    protected override async void OnNewIntent(Intent intent)
    {
        base.OnNewIntent(intent);
        
        var code = intent.Data.GetQueryParameter("code");
        
        var parameters = new Dictionary<string, string>
        {
            { "code", code },
            { "redirect_uri", "pp://auth" },
            { "grant_type", "authorization_code" }
        };
        
        var response = await MauiProgram.SpotifyAuth.PostAsync(
            "/api/token", new FormUrlEncodedContent(parameters));
        
        MauiProgram.TokenData = JsonSerializer.Deserialize<TokenModel>(await response.Content.ReadAsStringAsync());
    }
}