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
                //sprawdz date
            }
                break;
            case (false, true):
            {
                //send refreshToken
            }
                break;
            case (true, true):
            {
                //sprawdz date
            }
                break;
            
        }
    }

    private static async Task CheckTokenExpireDate(bool accessTokenExists, 
        bool refreshTokenExists)
    {
        var expireDateString = await SecureStorage.Default.GetAsync(AuthParamsInfo.ExpireDateParamName);
        if (string.IsNullOrWhiteSpace(expireDateString))
        {
            CleanTokenSecureStorage();
            await OpenAuthBrowser();
            return;
        }

        var expireDate = DateTimeOffset.Parse(expireDateString);
        if (expireDate < TimeProvider.System.GetLocalNow())
        {
            if (!refreshTokenExists)
            {
                CleanTokenSecureStorage();
                await OpenAuthBrowser();
                return;
            }
            
            //wysłanie refreshToken
            return;
        }
        else
        {
            if (accessTokenExists) return;
            CleanTokenSecureStorage();
            await OpenAuthBrowser();
            return;
        }
    }

    private static Task SendRefreshToken()
    {
        
    }

    public static async Task OpenAuthBrowser()
    {
        await Browser.Default.OpenAsync(GetCodeAuthUri(), BrowserLaunchMode.SystemPreferred);
    }
}