namespace PlacePlays.Mobile;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
        
       Browser.Default.OpenAsync(new Uri("https://accounts.spotify.com/authorize?response_type=token&client_id=-&scope=user-read-currently-playing&redirect_uri=pp://auth&state=123"),BrowserLaunchMode.SystemPreferred);
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