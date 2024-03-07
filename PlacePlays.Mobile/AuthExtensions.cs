using System.Text;

namespace PlacePlays.Mobile;

public static class AuthExtensions
{
    public static string GetBase64String(string clientId, string clientSecret)
    {
        var bytes = Encoding.Default.GetBytes(clientId + ':' + clientSecret);
        return Convert.ToBase64String(bytes);
    }
}