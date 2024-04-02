using PlacePlays.Domain.Entities;
using PlacePlays.Infrastructure.Models;

namespace PlacePlays.Infrastructure.Mapper;

public static class SpotifyMapper
{
    public static SpotifyEntity Map(this SpotifyTrackInfo trackInfo)
    {
        return new SpotifyEntity
        {
            Id = trackInfo.Id,
            TrackId = trackInfo.TrackId,
            Latitude = trackInfo.Latitude,
            Longitude = trackInfo.Longitude,
            Date = trackInfo.Date
        };
    }
}