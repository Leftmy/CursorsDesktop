using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
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