using PlacePlays.Application.Models;

namespace PlacePlays.Infrastructure.DAL;

public interface ISpotifyRepository
{
    ValueTask AddTrackWithGps(SpotifyLiteDbModel item);
}