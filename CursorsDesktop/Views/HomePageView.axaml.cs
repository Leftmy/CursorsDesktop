using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CursorsDesktop.Entities;
using CursorsDesktop.Services;
using CursorsDesktop.ViewModels;

namespace CursorsDesktop.Views;

public partial class HomePageView : UserControl
{
    public HomePageView()
    {
        InitializeComponent();
    }
    private void Button_Click(object? sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var package = button?.DataContext as Package;
        //button.Content = package.PackagePath;

        PackageService packageService = new PackageService();
        Package apackage = packageService.getPackage(package.PackageName);

        packageService.setPackage(apackage);
    }
}