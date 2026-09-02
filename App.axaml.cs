using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CursorsDesktop.Abstractions.Clients;
using CursorsDesktop.Abstractions.Services;
using CursorsDesktop.Clients;
using CursorsDesktop.Data;
using CursorsDesktop.Services;
using CursorsDesktop.ViewModels;
using CursorsDesktop.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CursorsDesktop;

public partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; private set; } = null!;
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        using (var db = new ApplicationDbContext())
        {
            db.Database.Migrate();
        }

        var services = new ServiceCollection();

        services.AddTransient<IPackagesService, PackagesService>();
        services.AddTransient<ICursorTypesService, CursorTypesService>();
        
        services.AddTransient<MainViewModel>();

        services.AddHttpClient<ICursorTypesClient, CursorTypesClient>( client =>
        {
            client.BaseAddress = new Uri(AppConstants.ApiBaseUrl);
        });

        ServiceProvider = services.BuildServiceProvider();

        Task.Run(async () =>
        {
            try
            {
                var cursorTypesService = ServiceProvider.GetRequiredService<ICursorTypesService>();
                await cursorTypesService.SyncCursorTypesAsync();
                Debug.WriteLine("Cursor types synchronized successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error syncing cursor types: {ex.Message}");
            }
        });

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = ServiceProvider.GetRequiredService<MainViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}