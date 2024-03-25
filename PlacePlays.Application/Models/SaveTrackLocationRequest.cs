namespace PlacePlays.Application.Models;

public class SaveTrackLocationRequest
{
    public string Id { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public DateTime AddDate { get; set; }
};