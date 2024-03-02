namespace PlacePlays.Infrastructure.DAL.Repositories;

public class SpotifyRepository(HttpClient spotifyClient) : ISpotifyRepository
{
    private readonly HttpClient _spotifyClient = spotifyClient;

    public void GetTestInfo()
    {
        //_spotifyClient.GetAsync();
    }
}