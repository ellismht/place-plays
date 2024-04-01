using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PlacePlays.Infrastructure.Models;

public class SpotifyEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string TrackId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime Date { get; set; }
}
