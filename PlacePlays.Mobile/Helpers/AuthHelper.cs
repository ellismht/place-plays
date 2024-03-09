using System.Net.Http.Headers;
using System.Net.Http.Json;
using PlacePlays.Mobile.Models;

namespace PlacePlays.Mobile.Helpers;

public static class AuthHelper
{
    private static Uri GetCodeAuthUri()
    {
        var parameters = new Dictionary<string, string>()
        {
            { AuthParamsInfo.ResponseTypeParamName, App.AuthOptions.ResponseType },
            { AuthParamsInfo.ClientIdParamName, App.AuthOptions.ClientId },
            { AuthParamsInfo.ScopeParamName, App.AuthOptions.Scope },
            { AuthParamsInfo.RedirectUriParamName, App.AuthOptions.RedirectUri },
            { AuthParamsInfo.StateParamName, "123" }
        };

        var address = new UriBuilder(App.AuthOptions.AuthAddress)
        {
            Query = string.Join("&", parameters.Select(kvp => $"{kvp.Key}={kvp.Value}"))
        };

        return address.Uri;
    }
    private static FormUrlEncodedContent GetAuthCodeParams(string code)
    {
        var parameters = new Dictionary<string, string>
        {
            { AuthParamsInfo.CodeParamName, code },
            { AuthParamsInfo.RedirectUriParamName, App.AuthOptions.RedirectUri},
            { AuthParamsInfo.GrantTypeParamName, App.AuthOptions.AuthCodeGrantType }
        };

        return new FormUrlEncodedContent(parameters);
    }
    
    private static FormUrlEncodedContent GetRefreshTokenParams(string refreshToken)
    {
        var parameters = new Dictionary<string, string>
        {
            { AuthParamsInfo.GrantTypeParamName, App.AuthOptions.RefreshTokenGrantType },
            { AuthParamsInfo.RefreshTokenParamName, refreshToken}
        };

        return new FormUrlEncodedContent(parameters);
    }
    
    private static async Task SetTokenSecureStorage(TokenDataModel tokenData)
    {
        await SecureStorage.Default.SetAsync(AuthParamsInfo.AccessTokenParamName, tokenData.AccessToken);
        await SecureStorage.Default.SetAsync(AuthParamsInfo.RefreshTokenParamName, tokenData.RefreshToken);
        await SecureStorage.Default.SetAsync(AuthParamsInfo.ExpireDateParamName, tokenData.ExpireDate.ToString());
    }
    
    private static void CleanTokenSecureStorage()
    {
        SecureStorage.Default.Remove(AuthParamsInfo.AccessTokenParamName);
        SecureStorage.Default.Remove(AuthParamsInfo.RefreshTokenParamName);
        SecureStorage.Default.Remove(AuthParamsInfo.ExpireDateParamName);
    }

    public static async Task<bool> PostForToken(string code)
    {
        var response = await App.SpotifyAuth.PostAsync(
            "/api/token", GetAuthCodeParams(code));

        if (!response.IsSuccessStatusCode) return false;
        
        var tokenData = await response.Content.ReadFromJsonAsync<TokenDataModel>();

        await SetTokenSecureStorage(tokenData);
        SetAuthHeader(tokenData.AccessToken);

        return true;
    }

    public static async Task GetAccessToken()
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

    private static async Task CheckTokenExpireDate(bool accessTokenExists, 
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

    private static async Task<bool> PostRefreshToken(string refreshToken)
    {
        var response = await App.SpotifyAuth.PostAsync(
            "/api/token", GetRefreshTokenParams(refreshToken));
        if (!response.IsSuccessStatusCode) return false;

        var tokenData = await response.Content.ReadFromJsonAsync<TokenDataModel>();
        tokenData.RefreshToken = refreshToken;
        
        await SetTokenSecureStorage(tokenData);
        SetAuthHeader(tokenData.AccessToken);
        
        return true;
    }

    private static async Task OpenAuthBrowser()
    {
        await Browser.Default.OpenAsync(GetCodeAuthUri(), BrowserLaunchMode.SystemPreferred);
    }

    private static void SetAuthHeader(string accessToken)
    {
        App.SpotifyClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            App.AuthOptions.TokenType, accessToken);
    }
}