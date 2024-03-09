using System.Text.Json.Serialization;

namespace PlacePlays.Mobile.Models;

public class TokenDataModel
{
    private readonly DateTimeOffset _now = TimeProvider.System.GetLocalNow();
    
    [JsonPropertyName(AuthParamsInfo.AccessTokenParamName)]
    public string AccessToken { get; init; }
    [JsonPropertyName(AuthParamsInfo.ExpiresInParamName)]
    public int ExpiresIn { get; init; }
    [JsonPropertyName(AuthParamsInfo.RefreshTokenParamName)]
    public string RefreshToken { get; set; }

    public DateTimeOffset ExpireDate => _now.AddSeconds(ExpiresIn);

};
