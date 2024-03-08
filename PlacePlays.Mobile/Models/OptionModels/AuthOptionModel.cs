namespace PlacePlays.Mobile.Models.OptionModels;

public class AuthOptionModel
{
    public string BaseAuthAddress { get; set; }
    public string AuthAddress { get; set; }
    public string TokenAddress { get; set; }
    public string ResponseType { get; set; }
    public string AuthCodeGrantType { get; set; }
    public string RefreshTokenGrantType { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Scope { get; set; }
    public string RedirectUri { get; set; }
}