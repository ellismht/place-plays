using PlacePlays.Mobile.Models.ApiRequestModels;
using PlacePlays.Mobile.Models.ApiResponseModels;
using PlacePlays.Mobile.Models.Spotify;

namespace PlacePlays.Mobile.Services.Api;

public interface IApiService
{
    ValueTask PostSpotifyRecord(SpotifyRecord record);
    ValueTask<IEnumerable<SpotifyTrackInfo>> GetTracksInUserLocation(SpotifyTracksInAreaSettings tracksInAreaSettings);
}