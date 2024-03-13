using PlacePlays.Mobile.Services.Spotify;
using PlacePlays.Mobile.ViewModels;

namespace PlacePlays.Mobile.Pages;

public partial class MainPage : ContentPage
{
    private readonly IClientService _client;

    public MainPage(IClientService client, MainViewModel viewModel)
    {
        InitializeComponent();
        _client = client;

        BindingContext = viewModel;
        Title = "Main Page";
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var currentlyPlayingTrack = await _client.GetCurrentlyPlayingTrack();
        
        var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(5));

        var cancelTokenSource = new CancellationTokenSource();

        var location = await Geolocation.Default.GetLocationAsync(request, cancelTokenSource.Token);
    }
}