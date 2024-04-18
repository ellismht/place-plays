namespace PlacePlays.WebApi.Requests;

public record GetTracksInAreaRequest(double Lat, double Lon, int Radius);