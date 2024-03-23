using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlacePlays.Mobile.Models.Spotify;
using PlacePlays.Mobile.Services.Spotify;
using Shiny.Locations;

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

        TrackName = track.Item.Name;
        ArtistName = track.Item.Artists[0].Name;
        MainInfo = string.Empty;
    }

    private static async Task CheckPermission()
    {
        while (true)
        {
            var status = await Permissions.RequestAsync<Permissions.LocationAlways>();

            if (status is PermissionStatus.Granted) return;
        }
    }
}
