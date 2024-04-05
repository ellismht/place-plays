using PlacePlays.Domain.Entities;

namespace PlacePlays.Application.Services.Spotify;

public interface ISpotifyService
{
    ValueTask InsertTrackInfo(SpotifyTrackInfo trackInfo);
    ValueTask<IEnumerable<SpotifyTrackInfo>> GetTracksInArea();
}