using PlacePlays.Application.Models;
using PlacePlays.Application.Models.LiteDb;

namespace PlacePlays.Infrastructure.DAL;

public interface ISpotifyRepository
{
    ValueTask AddTrackWithGps(SpotifyLiteDbModel item);
}