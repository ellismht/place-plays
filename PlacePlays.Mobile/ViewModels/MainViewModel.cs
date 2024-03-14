using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        var track = await _client.GetCurrentlyPlayingTrack();

        if (track.Item is null)
        {
            mainInfo = "Niczego nie słuchasz";
            return;
        }

        trackName = track.Item.Name;
        artistName = track.Item.Artists[0].Name;
        mainInfo = string.Empty;
    }
}
