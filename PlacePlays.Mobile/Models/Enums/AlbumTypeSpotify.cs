using System.Text.Json.Serialization;

namespace PlacePlays.Mobile.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter<AlbumTypeSpotify>))]
public enum AlbumTypeSpotify
{ 
    Album, 
    Single, 
    Compilation
}