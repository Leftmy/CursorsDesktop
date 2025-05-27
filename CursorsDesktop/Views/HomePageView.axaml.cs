using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
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
    private void OnSortButtonClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var button = sender as Button;
        var icon = button?.Content as PathIcon;
        Application.Current!.TryFindResource("SortAscendingRegular", out var res1);
        Application.Current!.TryFindResource("SortDescendingRegular", out var res2);

        if (icon != null)
        {
            if (icon.Data == (StreamGeometry)res1)
            {
                icon.Data = (StreamGeometry)res2;
            }
            else
            {
                icon.Data = (StreamGeometry)res1;
            }
        }
    }
}