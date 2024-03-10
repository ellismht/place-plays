namespace PlacePlays.Mobile.Services.Spotify;

public class SpotifyClientService : IClientService
{
    private const string BaseRequestUri = "/v1/me/player";
    
    public ValueTask GetCurrentlyPlayingTrack()
    {
        
    }
}