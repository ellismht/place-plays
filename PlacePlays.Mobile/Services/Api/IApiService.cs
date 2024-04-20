using PlacePlays.Domain.Entities;
using PlacePlays.Mobile.Models.Spotify;

namespace PlacePlays.Mobile.Services.Api;

public interface IApiService
{
    ValueTask PostSpotifyRecord(SpotifyRecord record);
    ValueTask<IEnumerable<SpotifyTrackInfo>> GetTracksInUserLocation(SpotifyTracksInAreaSettings tracksInAreaSettings);
}