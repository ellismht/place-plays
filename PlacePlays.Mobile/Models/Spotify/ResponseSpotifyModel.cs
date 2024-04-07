using System.Text.Json.Serialization;

namespace PlacePlays.Mobile.Models.Spotify;

public class ResponseSpotifyModel
{
    public ContextSpotifyModel Context { get; set; }
    [JsonPropertyName("progress_ms")]
    public int ProgressMs { get; set; }
    public ItemSpotifyModel Item { get; set; }
    public int TrackRemainingMs => Item?.DurationMs - ProgressMs ?? 0;
}