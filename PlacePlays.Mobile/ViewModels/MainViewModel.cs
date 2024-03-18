using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlacePlays.Mobile.Models.Spotify;
using PlacePlays.Mobile.Services.Spotify;

namespace PlacePlays.Mobile.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly IClientService _client;

    public MainViewModel(IClientService client)
    {
        _client = client;
        Title = "Main Page";
        mainInfo = "Nie wiem czego słuchasz";
    }

    [ObservableProperty] 
    string trackName;
    [ObservableProperty]
    string artistName;
    [ObservableProperty] 
    string mainInfo;

    [RelayCommand]
    private async Task GetCurrentlyPlayingTrack()
    {
        await CheckPermission();
        
        var track = await _client.GetCurrentlyPlayingTrack();
        
        if (track.Item is null)
        {
            MainInfo = "Niczego nie słuchasz";
            return;
        }
        
        var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(1));

        var _cancelTokenSource = new CancellationTokenSource();

        var location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

        var item = new SpotifyRecord(track.Item.Id, location.Latitude, location.Longitude,
            TimeProvider.System.GetLocalNow());

        TrackName = track.Item.Name;
        ArtistName = track.Item.Artists[0].Name;
        MainInfo = string.Empty;
    }

    private async Task CheckPermission()
    {
        var status = await Permissions.RequestAsync<Permissions.LocationAlways>();

        if (status is PermissionStatus.Granted) return;

        await CheckPermission();
    }
}
