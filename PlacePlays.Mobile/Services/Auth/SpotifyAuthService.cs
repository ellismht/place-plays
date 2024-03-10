using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using PlacePlays.Mobile.Models;
using PlacePlays.Mobile.Models.OptionModels;
using PlacePlays.Mobile.Pages;

namespace PlacePlays.Mobile.Services.Auth;

public class SpotifyAuthService : BaseAuthService, IAuthService
{
    private const string RequestUri = "/api/token";
    private readonly AuthOptionModel _authOptions;

    public SpotifyAuthService(IOptions<AuthOptionModel> authOptions)
    {
        _authOptions = authOptions.Value;
    }
    
    public async Task GetAccessToken()
    {
        var accessToken = await SecureStorage.Default.GetAsync(AuthParamsInfo.AccessTokenParamName);
        var refreshToken = await SecureStorage.Default.GetAsync(AuthParamsInfo.RefreshTokenParamName);

        var accessTokenExists = !string.IsNullOrWhiteSpace(accessToken);
        var refreshTokenExists = !string.IsNullOrWhiteSpace(refreshToken);

        switch (accessTokenExists, refreshTokenExists)
        {
            case (false, false):
            {
                CleanTokenSecureStorage();
                await OpenAuthBrowser();
            }
                break;
            case(true, false):
            {
                await CheckTokenExpireDate(true, false, accessToken, refreshToken);
            }
                break;
            case (false, true):
            {
                var isSuccess = await PostRefreshToken(refreshToken);
                if (isSuccess)
                {
                    await Shell.Current.GoToAsync(nameof(MainPage));
                    return;
                }
                
                CleanTokenSecureStorage();
                await OpenAuthBrowser();
            }
                break;
            case (true, true):
            {
                await CheckTokenExpireDate(true, true, accessToken, refreshToken);
            }
                break;
            
        }
    }
    
    public async Task<bool> PostForToken(string code)
    {
        var response = await App.SpotifyAuth.PostAsync(
            RequestUri, GetAuthCodeParams(code));

        if (!response.IsSuccessStatusCode) return false;
        
        var tokenData = await response.Content.ReadFromJsonAsync<TokenDataModel>();

        await SetTokenSecureStorage(tokenData);
        SetAuthHeader(tokenData.AccessToken);

        return true;
    }

    private async Task<bool> PostRefreshToken(string refreshToken)
    {
        var response = await App.SpotifyAuth.PostAsync(
            RequestUri, GetRefreshTokenParams(refreshToken));
        if (!response.IsSuccessStatusCode) return false;

        var tokenData = await response.Content.ReadFromJsonAsync<TokenDataModel>();
        tokenData.RefreshToken = refreshToken;
        
        await SetTokenSecureStorage(tokenData);
        SetAuthHeader(tokenData.AccessToken);
        
        return true;
    }
    
    private async Task CheckTokenExpireDate(bool accessTokenExists, 
        bool refreshTokenExists, string accessToken, string refreshToken)
    {
        var expireDateString = await SecureStorage.Default.GetAsync(AuthParamsInfo.ExpireDateParamName);
        if (string.IsNullOrWhiteSpace(expireDateString))
        {
            CleanTokenSecureStorage();
            await OpenAuthBrowser();
            return;
        }

        var expireDate = DateTimeOffset.Parse(expireDateString);
        if (expireDate < TimeProvider.System.GetLocalNow() && refreshTokenExists)
        {
            var isSuccess = await PostRefreshToken(refreshToken);
            if (isSuccess)
            {
                await Shell.Current.GoToAsync(nameof(MainPage));
                return;
            }
        }
        else
        {
            if (accessTokenExists && refreshTokenExists)
            {
                SetAuthHeader(accessToken);
                await Shell.Current.GoToAsync(nameof(MainPage));
                return;
            }
        }
        
        CleanTokenSecureStorage();
        await OpenAuthBrowser();
    }
    
    private Uri GetCodeAuthUri()
    {
        CreateState(16);
        var parameters = new Dictionary<string, string>()
        {
            { AuthParamsInfo.ResponseTypeParamName, _authOptions.ResponseType },
            { AuthParamsInfo.ClientIdParamName, _authOptions.ClientId },
            { AuthParamsInfo.ScopeParamName, _authOptions.Scope },
            { AuthParamsInfo.RedirectUriParamName, _authOptions.RedirectUri },
            { AuthParamsInfo.StateParamName, GetState() }
        };

        var address = new UriBuilder(_authOptions.AuthAddress)
        {
            Query = string.Join("&", parameters.Select(kvp => $"{kvp.Key}={kvp.Value}"))
        };

        return address.Uri;
    }
    private FormUrlEncodedContent GetAuthCodeParams(string code)
    {
        var parameters = new Dictionary<string, string>
        {
            { AuthParamsInfo.CodeParamName, code },
            { AuthParamsInfo.RedirectUriParamName, _authOptions.RedirectUri},
            { AuthParamsInfo.GrantTypeParamName, _authOptions.AuthCodeGrantType }
        };

        return new FormUrlEncodedContent(parameters);
    }
    
    private FormUrlEncodedContent GetRefreshTokenParams(string refreshToken)
    {
        var parameters = new Dictionary<string, string>
        {
            { AuthParamsInfo.GrantTypeParamName, _authOptions.RefreshTokenGrantType },
            { AuthParamsInfo.RefreshTokenParamName, refreshToken}
        };

        return new FormUrlEncodedContent(parameters);
    }

    private async Task OpenAuthBrowser()
    {
        await Browser.Default.OpenAsync(GetCodeAuthUri(), BrowserLaunchMode.SystemPreferred);
    }

    private void SetAuthHeader(string accessToken)
    {
        App.SpotifyClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            _authOptions.TokenType, accessToken);
    }
}