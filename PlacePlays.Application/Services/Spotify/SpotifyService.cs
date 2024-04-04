using PlacePlays.Application.Abstractions;
using PlacePlays.Application.Services.Math;
using PlacePlays.Domain.Entities;

namespace PlacePlays.Application.Services.Spotify;

public class SpotifyService : ISpotifyService
{
    private readonly IRepository _repository;
    private readonly IMathService _mathService;

    public SpotifyService(IRepository repository, IMathService mathService)
    {
        _repository = repository;
        _mathService = mathService;
    }
    
    public async ValueTask InsertTrackInfo(SpotifyTrackInfo trackInfo)
    {
        await _repository.InsertTrackInfo(trackInfo);
    }

    public async ValueTask<IEnumerable<SpotifyTrackInfo>> GetTracksInArea()
    {


        return await _repository.GetTracksInArea();
    }
}