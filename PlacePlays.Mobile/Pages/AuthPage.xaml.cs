using PlacePlays.Mobile.Services.Auth;
using Shiny;
using Shiny.Locations;

namespace PlacePlays.Mobile.Pages;

public partial class AuthPage : ContentPage
{
    private readonly IAuthService _authService;
    private readonly IGpsManager _gpsManager;

    public AuthPage(IAuthService authService, IGpsManager gpsManager)
    {
        _authService = authService;
        _gpsManager = gpsManager;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await CheckPermission();

        if (!_gpsManager.IsListening())
        {
            var accessState = await _gpsManager.RequestAccess(new GpsRequest
            {
                BackgroundMode = GpsBackgroundMode.Realtime
            });

            if (accessState == AccessState.Available)
            {
                await _gpsManager.StartListener(new GpsRequest
                {
                    BackgroundMode = GpsBackgroundMode.Realtime
                });
            }
        }
        
        await _authService.GetAccessToken();
    }
    
    private async Task CheckPermission()
    {
        var status = await Permissions.RequestAsync<Permissions.LocationAlways>();
        status = await Permissions.RequestAsync<Permissions.PostNotifications>();

        if (status is PermissionStatus.Granted) return;

        await CheckPermission();
    }
}