using PlacePlays.Mobile.Pages;

namespace PlacePlays.Mobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(AuthPage), typeof(AuthPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}