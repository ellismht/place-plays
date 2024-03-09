namespace PlacePlays.Mobile.Models.OptionModels;

public class AuthOptionModel
{
    public string BaseAddress { get; init; }
    public string AuthAddress { get; init; }
    public string TokenAddress { get; init; }
    public string ResponseType { get; init; }
    public string AuthCodeGrantType { get; init; }
    public string RefreshTokenGrantType { get; init; }
    public string ClientId { get; init; }
    public string ClientSecret { get; init; }
    public string Scope { get; init; }
    public string RedirectUri { get; init; }
    public string TokenType { get; init; }
}