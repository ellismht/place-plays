using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlacePlays.Mobile.Models.ApiRequestModels;
using PlacePlays.Mobile.Services.Api;
using PlacePlays.Mobile.Services.Spotify;

namespace PlacePlays.Mobile.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly IClientService _client;
    private readonly IApiService _api;

    public MainViewModel(IClientService client, IApiService api)
    {
        _client = client;
        _api = api;
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
        var track = await _client.GetCurrentlyPlayingTrack();
        
        if (track.Item is null)
        {
            MainInfo = "Niczego nie słuchasz";
            return;
        }

        TrackName = track.Item.Name;
        ArtistName = track.Item.Artists[0].Name;
        MainInfo = string.Empty;
    }
    
    [RelayCommand]
    private async Task GetTracksInArea()
    {
        var settings = new SpotifyTracksInAreaSettings(37.4220936, -122.083922, 50);
        var tracks = await _api.GetTracksInUserLocation(settings);
    }
}
