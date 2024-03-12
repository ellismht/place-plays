using PlacePlays.Mobile.Services.Spotify;

namespace PlacePlays.Mobile.Pages;

public partial class MainPage : ContentPage
{
    private readonly IClientService _client;
    int count = 0;

    public MainPage(IClientService client)
    {
        _client = client;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var currentlyPlayingTrack = await _client.GetCurrentlyPlayingTrack();
        
        var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(5));

        var cancelTokenSource = new CancellationTokenSource();

        var location = await Geolocation.Default.GetLocationAsync(request, cancelTokenSource.Token);
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}