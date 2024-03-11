using System.Text.Json.Serialization;

namespace PlacePlays.Mobile.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter<ObjectTypeSpotify>))]
public enum ObjectTypeSpotify
{
    Artist,
    Album,
    Track
}