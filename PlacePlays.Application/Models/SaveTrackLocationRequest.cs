namespace PlacePlays.Application.Models;

public record SaveTrackLocationRequest(string Id, double Lat, double Lon, DateTime AddDate);