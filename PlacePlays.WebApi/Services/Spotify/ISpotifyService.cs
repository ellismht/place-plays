using PlacePlays.WebApi.Entities;

namespace PlacePlays.WebApi.Services.Spotify;

public interface ISpotifyService
{
    ValueTask InsertTrackInfo(SpotifyTrackInfo trackInfo);
    ValueTask<IEnumerable<SpotifyTrackInfo>> GetTracksInArea(SpotifyTracksInAreaSettings tracksInAreaSettings);
}