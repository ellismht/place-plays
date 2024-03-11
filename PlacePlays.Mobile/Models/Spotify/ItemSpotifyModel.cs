using System.Text.Json.Serialization;
using PlacePlays.Mobile.Models.Enums;

namespace PlacePlays.Mobile.Models.Spotify;

public class ItemSpotifyModel
{
    public AlbumSpotifyModel Album { get; set; }
    public IReadOnlyList<ArtistSpotifyModel> Artists { get; set; }
    [JsonPropertyName("external_urls")]
    public ExternalUrls ExternalUrls { get; set; }
    public Uri Href { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    [JsonPropertyName("track_number")]
    public int TrackNumber { get; set; }
    public ObjectTypeSpotify ObjectType { get; set; }
    public Uri Uri { get; set; }
}