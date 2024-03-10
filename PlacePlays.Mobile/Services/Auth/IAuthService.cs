namespace PlacePlays.Mobile.Services.Auth;

public interface IAuthService
{
    Task<bool> PostForToken(string code);
    Task GetAccessToken();
    string GetState();
}