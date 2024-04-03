namespace PlacePlays.WebApi.Requests;

public record GetTracksInAreaRequest(double Lon, double Lat, int Radius);