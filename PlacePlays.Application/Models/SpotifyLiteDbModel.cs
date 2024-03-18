namespace PlacePlays.Application.Models;

public class SpotifyLiteDbModel
{
    public string Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTimeOffset Date { get; set; }
}