using Android.App;
using Android.Content;
using Android.Content.PM;
using PlacePlays.Mobile.Helpers;
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
        
        var code = intent.Data.GetQueryParameter(AuthParamsInfo.CodeParamName);
        
        var response = !string.IsNullOrWhiteSpace(code) && await AuthHelper.PostForToken(code);

        if (!response || Intent.ActionView != intent.Action)
            throw new Exception();
    }
}