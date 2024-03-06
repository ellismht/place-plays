using Microsoft.Extensions.Configuration;
using Org.Apache.Http.Authentication;

namespace PlacePlays.Mobile;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage(IConfiguration config)
    {
        InitializeComponent();

        var address = new Uri(config["Auth:Address"] + 
                              "?response_type=" + config["Auth:ResponseType"] +
                              "&client_id=" + config["Auth:ClientId"] + 
                              "&scope=" + config["Auth:Scope"] + 
                              "&redirect_uri=" + config["Auth:RedirectUri"] +
                              "&state=" + "123"
                              );
        Browser.Default.OpenAsync(address, BrowserLaunchMode.SystemPreferred);
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}