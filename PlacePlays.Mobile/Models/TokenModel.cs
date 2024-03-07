using System.Text.Json.Serialization;

namespace PlacePlays.Mobile.Models;

public class TokenModel
{
    private DateTime Now = DateTime.Now;
    
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; }
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init; }
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; init; }

    public DateTime ExpireDate => Now.AddSeconds(ExpiresIn);

};
