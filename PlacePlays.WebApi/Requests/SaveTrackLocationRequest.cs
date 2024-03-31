namespace PlacePlays.WebApi.Requests;

public record SaveTrackLocationRequest(string Id, double Lat, double Lon, DateTime AddDate);