using PlacePlays.Application.Abstractions;
using PlacePlays.Application.Services.Math;
using PlacePlays.Domain.Entities;

namespace PlacePlays.Application.Services.Spotify;

public class SpotifyService : ISpotifyService
{
    private readonly IRepository _repository;

    public SpotifyService(IRepository repository)
    {
        _repository = repository;
    }
    
    public async ValueTask InsertTrackInfo(SpotifyTrackInfo trackInfo)
    {
        await _repository.InsertTrackInfo(trackInfo);
    }

    public async ValueTask<IEnumerable<SpotifyTrackInfo>> GetTracksInArea(SpotifyTracksInAreaSettings tracksInAreaSettings)
    {
        

        return await _repository.GetTracksInArea(tracksInAreaSettings);
    }
}