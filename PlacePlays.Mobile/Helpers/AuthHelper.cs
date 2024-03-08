using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using PlacePlays.Mobile.Models;
using PlacePlays.Mobile.Models.OptionModels;

namespace PlacePlays.Mobile.Helpers;

public class AuthHelper
{
    private static FormUrlEncodedContent GetAuthCodeParams(string code)
    {
        var parameters = new Dictionary<string, string>
        {
            { AuthParamsInfo.CodeParamName, code },
            { AuthParamsInfo.RedirectUriParamName, MauiProgram.AuthOptions.RedirectUri},
            { AuthParamsInfo.GrantTypeParamName, MauiProgram.AuthOptions.AuthCodeGrantType }
        };

        return new FormUrlEncodedContent(parameters);
    }
    
    private static async Task SetTokenSecureStorage(TokenDataModel tokenData)
    {
        await SecureStorage.Default.SetAsync(AuthParamsInfo.AccessTokenParamName, tokenData.AccessToken);
        await SecureStorage.Default.SetAsync(AuthParamsInfo.RefreshTokenParamName, tokenData.RefreshToken);
        await SecureStorage.Default.SetAsync(AuthParamsInfo.ExpireDateParamName, tokenData.ExpireDate.ToString());
    }

    public static async Task<bool> PostForToken(string code)
    {
        var response = await MauiProgram.SpotifyAuth.PostAsync(
            "/api/token", GetAuthCodeParams(code));

        if (!response.IsSuccessStatusCode) return false;
        
        var tokenData = await response.Content.ReadFromJsonAsync<TokenDataModel>();

        await SetTokenSecureStorage(tokenData);

        return true;
    }
}