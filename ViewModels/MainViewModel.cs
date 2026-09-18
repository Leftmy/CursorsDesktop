using CommunityToolkit.Mvvm.ComponentModel;
using CursorsDesktop.Abstractions.Services;

namespace CursorsDesktop.ViewModels;

public partial class MainViewModel(IPackagesService packageService) : ViewModelBase
{
    private readonly IPackagesService _packageService = packageService;
    [ObservableProperty]
    private string _greeting = "Welcome to Avalonia!";
}