using PlacePlays.Mobile.Helpers;

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
        
        await AuthHelper.GetAccessToken();
    }
}