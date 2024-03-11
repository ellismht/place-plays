namespace PlacePlays.Mobile.Services.Spotify;

public interface IClientService
{
    ValueTask GetCurrentlyPlayingTrack();
}