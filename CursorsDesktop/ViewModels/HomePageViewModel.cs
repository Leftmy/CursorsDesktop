using CommunityToolkit.Mvvm.ComponentModel;
using CursorsDesktop.Entities;
using CursorsDesktop.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CursorsDesktop.ViewModels
{
    internal partial class HomePageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<Package> _packages;

        private string _customFilter;

        public ObservableCollection<Package> Packages
        {
            get => _packages;
            set
            {
                if (_packages != value)
                {
                    _packages = value;
                    OnPropertyChanged(nameof(Packages));
                }
            }
        }
        public string CustomFilter
        {
            get => _customFilter;
            set
            {
                if (_customFilter != value)
                {
                    _customFilter = value;
                    OnPropertyChanged(nameof(CustomFilter));
                    PackageService tmp = new();
                    Packages = tmp.findByName(_customFilter);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public HomePageViewModel()
        {
            PackageService tmp = new PackageService();
            Packages = tmp.GetPackages();
        }
    }
}
