using System.Net;
using System.Net.Http.Json;
using PlacePlays.Mobile.Models.Spotify;

namespace PlacePlays.Mobile.Services.Spotify;

public class SpotifyClientService : IClientService
{
    private const string BaseRequestUri = "/v1/me/player";
    
    public async ValueTask<ResponseSpotifyModel> GetCurrentlyPlayingTrack()
    {
        var response = await App.SpotifyClient.GetAsync(
            BaseRequestUri + "/currently-playing");

        if (response.StatusCode != HttpStatusCode.OK) 
            return new ResponseSpotifyModel();
        
        return await response.Content.ReadFromJsonAsync<ResponseSpotifyModel>();
    }
}