using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace CursorsDesktop.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsHomeActive))]
    [NotifyPropertyChangedFor(nameof(IsMyPacksActive))]
    [NotifyPropertyChangedFor(nameof(IsSettingsActive))]
    private object _currentPage = null!;


    public MainViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        NavigateToHome();
    }

    public bool IsHomeActive => CurrentPage is HomePageViewModel;
    public bool IsMyPacksActive => false;
    public bool IsSettingsActive => false;

    [RelayCommand]
    public void NavigateToHome()
    {
        CurrentPage = _serviceProvider.GetRequiredService<HomePageViewModel>();
    }

    [RelayCommand]
    public void NavigateToMyPacks()
    {
        // CurrentPage = _serviceProvider.GetRequiredService<MyPacksViewModel>();
    }

    [RelayCommand]
    public void NavigateToSettings()
    {
        // CurrentPage = _serviceProvider.GetRequiredService<SettingsViewModel>();
    }
}