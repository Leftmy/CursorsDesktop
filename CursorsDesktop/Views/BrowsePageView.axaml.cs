using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CursorsDesktop.Entities;
using CursorsDesktop.Services;
using CursorsDesktop.ViewModels;

namespace CursorsDesktop.Views;

public partial class BrowsePageView : UserControl
{
    public BrowsePageView()
    {
        InitializeComponent();
    }
    private void Download_Click(object? sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var package = button?.DataContext as Package;

        if (package != null)
        {
            PackageService tmp = new();
            tmp.downloadPackage(package.PackageId);
        }
    }
}