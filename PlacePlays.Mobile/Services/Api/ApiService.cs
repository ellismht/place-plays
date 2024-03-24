using System.Net;
using System.Net.Http.Json;
using PlacePlays.Mobile.Models.Spotify;

namespace PlacePlays.Mobile.Services.Api;

public class ApiService : IApiService
{
    private const string PostSpotifyRecordEndpoint = "someEndpoint";
    
    public async ValueTask PostSpotifyRecord(SpotifyRecord record)
    {
        await App.ApiClient.PostAsJsonAsync(PostSpotifyRecordEndpoint, record);
    }
}