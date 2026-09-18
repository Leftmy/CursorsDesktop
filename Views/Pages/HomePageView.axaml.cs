using Avalonia.Controls;
using CursorsDesktop.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CursorsDesktop.Views.Pages;

public partial class HomePageView : UserControl
{
    public HomePageView()
    {
        InitializeComponent();

        DataContext = App.ServiceProvider.GetRequiredService<HomePageViewModel>();
    }
}