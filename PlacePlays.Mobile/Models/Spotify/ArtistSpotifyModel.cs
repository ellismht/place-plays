using System.Text.Json.Serialization;
using PlacePlays.Mobile.Models.Enums;

namespace PlacePlays.Mobile.Models.Spotify;

public class ArtistSpotifyModel
{
    [JsonPropertyName("external_urls")]
    public ExternalUrls ExternalUrls { get; set; }
    public Uri Href { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public ObjectTypeSpotify ObjectType { get; set; }
    public Uri Uri { get; set; }
}