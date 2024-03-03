using System.Text;
using IdentityModel.OidcClient;

namespace PlacePlays.Mobile;

public partial class MainPage 
{
    private readonly OidcClient _client;
    private string _currentAccessToken;
    int count = 0;

    public MainPage(OidcClient client)
    {
        InitializeComponent();
        _client = client;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var result = await _client.LoginAsync();

        if (result.IsError)
        {
            editor.Text = result.Error;
            return;
        }

        _currentAccessToken = result.AccessToken;

        var sb = new StringBuilder(128);

        sb.AppendLine("claims:");
        foreach (var claim in result.User.Claims)
        {
            sb.AppendLine($"{claim.Type}: {claim.Value}");
        }

        sb.AppendLine();
        sb.AppendLine("access token:");
        sb.AppendLine(result.AccessToken);

        if (!string.IsNullOrWhiteSpace(result.RefreshToken))
        {
            sb.AppendLine();
            sb.AppendLine("access token:");
            sb.AppendLine(result.AccessToken);
        }

        editor.Text = sb.ToString();
        
        count++;

        CounterBtn.Text = count == 1 ? $"Clicked {count} time" : $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}