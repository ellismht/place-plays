using System.Text.Json.Serialization;

namespace PlacePlays.Mobile.Models;

public class TokenDataModel
{
    private DateTimeOffset Now = TimeProvider.System.GetLocalNow();
    
    [JsonPropertyName(AuthParamsInfo.AccessTokenParamName)]
    public string AccessToken { get; init; }
    [JsonPropertyName(AuthParamsInfo.ExpiresInParamName)]
    private int ExpiresIn { get; init; }
    [JsonPropertyName(AuthParamsInfo.RefreshTokenParamName)]
    public string RefreshToken { get; init; }

    public DateTimeOffset ExpireDate => Now.AddSeconds(ExpiresIn);

};
