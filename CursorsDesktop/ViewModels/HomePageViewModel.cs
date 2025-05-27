using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CursorsDesktop.Entities;
using CursorsDesktop.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace CursorsDesktop.ViewModels
{
    internal partial class HomePageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<Package> _packages;

        private string _customFilter;
        private bool _isAscending = false;

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
            Packages = tmp.sortByName(tmp.GetPackages(),1);
        }
        [RelayCommand]
        public void SortPackages()
        {
            PackageService tmp = new PackageService();

            if (_isAscending)
            {

                Packages = tmp.sortByName(Packages, 1);
            }
            else
            {
                Packages = tmp.sortByName(Packages, -1);
            }

            _isAscending = !_isAscending;
        }
    }
}
