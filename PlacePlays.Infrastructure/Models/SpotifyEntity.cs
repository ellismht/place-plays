using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PlacePlays.Infrastructure.Models;

internal class SpotifyEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTimeOffset Date { get; set; }
}
