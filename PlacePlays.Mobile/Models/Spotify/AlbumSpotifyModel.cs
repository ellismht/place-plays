using System.Text.Json.Serialization;
using PlacePlays.Mobile.Models.Enums;

namespace PlacePlays.Mobile.Models.Spotify;

public class AlbumSpotifyModel : BaseSpotifyModel
{
    [JsonPropertyName("album_type")]
    public AlbumTypeSpotify AlbumType { get; set; }
    public IReadOnlyList<ArtistSpotifyModel> Artists { get; set; }
    public string Id { get; set; }
    public IReadOnlyList<ImageSpotifyModel> Images { get; set; }
    public string Name { get; set; }
    [JsonPropertyName("release_date")]
    public DateOnly ReleaseDate { get; set; }
    [JsonPropertyName("total_tracks")]
    public int TotalTracks { get; set; } 
    public ObjectTypeSpotify ObjectType { get; set; }
}