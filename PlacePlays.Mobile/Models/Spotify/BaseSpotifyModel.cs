using System.Text.Json.Serialization;

namespace PlacePlays.Mobile.Models.Spotify;

public class BaseSpotifyModel
{
    [JsonPropertyName("external_urls")]
    public ExternalUrls ExternalUrls { get; set; }
    public Uri Href { get; set; }
    public Uri Uri { get; set; }
}