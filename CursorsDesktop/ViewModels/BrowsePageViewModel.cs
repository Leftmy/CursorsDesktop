using CursorsDesktop.Entities;
using CursorsDesktop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursorsDesktop.ViewModels
{
    public class BrowsePageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<Package> _packages;
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
        public BrowsePageViewModel()
        {
            PackageService tmp = new PackageService();
            Packages = tmp.getBrowsePackages();
        }
    }

}
