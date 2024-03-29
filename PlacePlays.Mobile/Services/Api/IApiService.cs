using System.Net;
using PlacePlays.Mobile.Models.Spotify;

namespace PlacePlays.Mobile.Services.Api;

public interface IApiService
{
    ValueTask PostSpotifyRecord(SpotifyRecord record);
}