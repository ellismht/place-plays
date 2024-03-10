using PlacePlays.Mobile.Services.Auth;

namespace PlacePlays.Mobile.Pages;

public partial class AuthPage : ContentPage
{
    private readonly IAuthService _authService;

    public AuthPage(IAuthService authService)
    {
        _authService = authService;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        await _authService.GetAccessToken();
    }
}