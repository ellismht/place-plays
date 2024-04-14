using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using PlacePlays.Mobile.Models.Spotify;

namespace PlacePlays.Mobile.Services.Api;

public class ApiService : IApiService
{
    private const string BaseGroupEndpoint = "/api";
    private const string PostSpotifyRecordEndpoint = "/saveTrackLocationInfo";
    
    public async ValueTask PostSpotifyRecord(SpotifyRecord record)
    {
        //TODO: remove
        try
        {
            var client = new HttpClient();
            var json = JsonSerializer.Serialize(record);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await client.PostAsync("http://10.0.2.2:5176/api/saveTrackLocationInfo", content);
        }
        catch(Exception e)
        {
            var smth = e;
            throw;
        }
        //TODO

        var response = await App.ApiClient.PostAsJsonAsync(BaseGroupEndpoint + PostSpotifyRecordEndpoint, record);
    }
}