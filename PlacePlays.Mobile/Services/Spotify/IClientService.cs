using PlacePlays.Mobile.Models.Spotify;

namespace PlacePlays.Mobile.Services.Spotify;

public interface IClientService
{
    ValueTask<ResponseSpotifyModel> GetCurrentlyPlayingTrack();
}