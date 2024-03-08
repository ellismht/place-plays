using Microsoft.Extensions.Options;
using PlacePlays.Mobile.Models;
using PlacePlays.Mobile.Models.OptionModels;

namespace PlacePlays.Mobile.Pages;

public partial class AuthPage : ContentPage
{
    public AuthPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var parameters = new Dictionary<string, string>()
        {
            { AuthParamsInfo.ResponseTypeParamName, MauiProgram.AuthOptions.ResponseType },
            { AuthParamsInfo.ClientIdParamName, MauiProgram.AuthOptions.ClientId },
            { AuthParamsInfo.ScopeParamName, MauiProgram.AuthOptions.Scope },
            { AuthParamsInfo.RedirectUriParamName, MauiProgram.AuthOptions.RedirectUri },
            { AuthParamsInfo.StateParamName, "123" }
        };

        var address = new UriBuilder(MauiProgram.AuthOptions.AuthAddress)
        {
            Query = string.Join("&", parameters.Select(kvp => $"{kvp.Key}={kvp.Value}"))
        };
        
        await Browser.Default.OpenAsync(address.Uri, BrowserLaunchMode.SystemPreferred);
    }
}