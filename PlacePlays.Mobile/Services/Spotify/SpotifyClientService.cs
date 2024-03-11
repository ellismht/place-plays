using System.Net.Http.Json;
using System.Text.Json;
using PlacePlays.Mobile.Models.Spotify;

namespace PlacePlays.Mobile.Services.Spotify;

public class SpotifyClientService : IClientService
{
    private const string BaseRequestUri = "/v1/me/player";
    
    public async ValueTask GetCurrentlyPlayingTrack()
    {
        var response = await App.SpotifyClient.GetAsync(
            BaseRequestUri + "/currently-playing");

        //var content = await response.Content.ReadAsStringAsync();
        var content = await response.Content.ReadFromJsonAsync<ResponseSpotifyModel>();

        //var smth = JsonSerializer.Deserialize<ResponseSpotifyModel>(content);
    }
}