using Android.App;
using Android.Content;
using Android.Content.PM;
using PlacePlays.Mobile.Models;
using PlacePlays.Mobile.Pages;
using PlacePlays.Mobile.Services.Auth;

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
        
        var code = intent.Data.GetQueryParameter(AuthParamsInfo.CodeParamName);
        var state = intent.Data.GetQueryParameter(AuthParamsInfo.StateParamName);

        var authService = IPlatformApplication.Current.Services.GetRequiredService<IAuthService>();
        
        var isSuccess = !string.IsNullOrWhiteSpace(code) 
                       && state != null
                       && await authService.PostForToken(code)
                       && state.Equals(authService.GetState());

        if (isSuccess && Intent.ActionView == intent.Action)
            await Shell.Current.GoToAsync(nameof(MainPage));
    }
}