using PlacePlays.Mobile.Models;

namespace PlacePlays.Mobile.Services.Auth;

public abstract class BaseAuthService
{
    private const string Characters = "abcdefghijklmnopqrstuvwxyz1234567890";
    private static string _state;
    protected static async Task SetTokenSecureStorage(TokenDataModel tokenData)
    {
        await SecureStorage.Default.SetAsync(AuthParamsInfo.AccessTokenParamName, tokenData.AccessToken);
        await SecureStorage.Default.SetAsync(AuthParamsInfo.RefreshTokenParamName, tokenData.RefreshToken);
        await SecureStorage.Default.SetAsync(AuthParamsInfo.ExpireDateParamName, tokenData.ExpireDate.ToString());
    }
    
    protected static void CleanTokenSecureStorage()
    {
        SecureStorage.Default.Remove(AuthParamsInfo.AccessTokenParamName);
        SecureStorage.Default.Remove(AuthParamsInfo.RefreshTokenParamName);
        SecureStorage.Default.Remove(AuthParamsInfo.ExpireDateParamName);
    }

    protected static void CreateState(int stateLength)
    {
        var stringChars = new char[stateLength];
        var random = new Random();

        for (var i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = Characters[random.Next(Characters.Length)];
        }

        _state = new string(stringChars);
    }

    public string GetState()
    {
        return _state;
    }
}