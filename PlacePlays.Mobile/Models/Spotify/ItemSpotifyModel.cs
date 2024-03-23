using System.Text.Json.Serialization;
using PlacePlays.Mobile.Models.Enums;

namespace PlacePlays.Mobile.Models.Spotify;

public class ItemSpotifyModel : BaseSpotifyModel
{
    public AlbumSpotifyModel Album { get; set; }
    public IReadOnlyList<ArtistSpotifyModel> Artists { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    [JsonPropertyName("track_number")]
    public int TrackNumber { get; set; }
    public ObjectTypeSpotify ObjectType { get; set; }
    [JsonPropertyName("duration_ms")]
    public int DurationMs { get; set; }
}