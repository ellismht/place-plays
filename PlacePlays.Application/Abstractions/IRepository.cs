using PlacePlays.Domain.Entities;

namespace PlacePlays.Application.Abstractions;

public interface IRepository
{
    ValueTask InsertTrackInfo(SpotifyTrackInfo trackInfo);
    ValueTask<IEnumerable<SpotifyTrackInfo>> GetTracksInArea(SpotifyTracksInAreaSettings tracksInAreaSettings);
}
