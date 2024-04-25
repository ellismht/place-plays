using PlacePlays.Mobile.Models.ApiResponseModels;
using System.Net.Http.Json;
using PlacePlays.Mobile.Models.ApiRequestModels;
using PlacePlays.Mobile.Models.Spotify;

namespace PlacePlays.Mobile.Services.Api;

public class ApiService : IApiService
{
    private const string BaseGroupEndpoint = "api";
    private const string PostSpotifyRecordEndpoint = "/saveTrackLocationInfo";
    
    public async ValueTask PostSpotifyRecord(SpotifyRecord record)
    {
        var response = await App.ApiClient.PostAsJsonAsync($"{BaseGroupEndpoint}/saveTrackLocationInfo", record);
    }

    public async ValueTask<IEnumerable<SpotifyTrackInfo>> GetTracksInUserLocation(SpotifyTracksInAreaSettings tracksInAreaSettings)
    {
        var response = await App.ApiClient.GetFromJsonAsync<IEnumerable<SpotifyTrackInfo>>
            ($"{BaseGroupEndpoint}/getTracksInArea?lat={tracksInAreaSettings.Lat}&lon={tracksInAreaSettings.Lon}&radius={tracksInAreaSettings.Radius}");

        return response;
    }
}