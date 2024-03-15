using PlacePlays.Mobile.Services.Spotify;
using PlacePlays.Mobile.ViewModels;

namespace PlacePlays.Mobile.Pages;

public partial class MainPage : ContentPage
{
    public MainPage(IClientService client, MainViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}