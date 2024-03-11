using System.Text.Json.Serialization;
using PlacePlays.Mobile.Models.Enums;

namespace PlacePlays.Mobile.Models.Spotify;

public class ContextSpotifyModel
{
    [JsonPropertyName("external_urls")]
    public ExternalUrls ExternalUrls { get; set; }
    public Uri Href { get; set; }
    public ObjectTypeSpotify ObjectType { get; set; }
    public Uri Uri { get; set; }
}