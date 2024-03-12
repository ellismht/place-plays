using PlacePlays.Mobile.Models.Enums;

namespace PlacePlays.Mobile.Models.Spotify;

public class ArtistSpotifyModel : BaseSpotifyModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ObjectTypeSpotify ObjectType { get; set; }
}