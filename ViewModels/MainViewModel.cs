using CommunityToolkit.Mvvm.ComponentModel;
using CursorsDesktop.Abstractions.Services;

namespace CursorsDesktop.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IPackagesService _packageService;
    [ObservableProperty]
    private string _greeting = "Welcome to Avalonia!";

    public MainViewModel(IPackagesService packageService)
    {
        _packageService = packageService;
    }
}