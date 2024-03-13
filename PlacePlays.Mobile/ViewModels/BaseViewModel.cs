using CommunityToolkit.Mvvm.ComponentModel;

namespace PlacePlays.Mobile.ViewModels;

public partial class BaseViewModel : ObservableObject
{ 
    [ObservableProperty] 
    private string title;
}