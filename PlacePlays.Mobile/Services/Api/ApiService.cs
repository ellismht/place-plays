using System.Net;
using System.Net.Http.Json;
using PlacePlays.Mobile.Models.Spotify;

namespace PlacePlays.Mobile.Services.Api;

public class ApiService : IApiService
{
    private const string BaseGroupEndpoint = "/api";
    private const string PostSpotifyRecordEndpoint = "/saveTrackLocationInfo";
    
    public async ValueTask PostSpotifyRecord(SpotifyRecord record)
    {
        var response = await App.ApiClient.PostAsJsonAsync(BaseGroupEndpoint + PostSpotifyRecordEndpoint, record);
    }
}