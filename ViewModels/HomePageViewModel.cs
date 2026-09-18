using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CursorsDesktop.Abstractions.Clients;
using CursorsDesktop.DTO.Packs;

namespace CursorsDesktop.ViewModels;

public partial class HomePageViewModel : ObservableObject
{
    private readonly IPackageClient _packageClient;

    [ObservableProperty]
    private ObservableCollection<PackagesGetAllResponse> _cursorPacks = [];

    public HomePageViewModel(IPackageClient client)
    {
        _packageClient = client;
        
        LoadPacksCommand.Execute(null); 
    }

    [RelayCommand]
    private async Task LoadPacksAsync()
    {
        var data = await _packageClient.GetAllAsync();
        
        CursorPacks = new ObservableCollection<PackagesGetAllResponse>(data);
    }
}