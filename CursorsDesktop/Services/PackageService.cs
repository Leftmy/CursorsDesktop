using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CursorsDesktop.Entities;
using Cursor = CursorsDesktop.Entities.Cursor;
using CursorsDesktop.Data;
using Avalonia.Animation;
using System.Collections.ObjectModel;


namespace CursorsDesktop.Services
{

    class PackageService
    {
        public PackageService()
        {

        }

        public Package getPackage(string name)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Package package = db.Packages.FirstOrDefault(p => p.PackageName == name);

                return package;
            }
        }


        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, IntPtr pvParam, uint fWinIni);


        const int SPI_SETCURSORS = 0x0057;
        const int SPIF_SENDCHANGE = 0x02;

        public void setPackage(Package package)
        {
            CursorService cursoreService = new CursorService();
            List<int> cursors = cursoreService.getCursors(package);
            foreach (int cursorId in cursors)
            {
                CursorService.setCursor(cursorId);
            }

            SystemParametersInfo(SPI_SETCURSORS, 0, IntPtr.Zero, SPIF_SENDCHANGE);

        }

        public void AddPackage(string name, string desription, string path)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Packages.Add(
                    new Package()
                    {
                        PackageName = name,
                        PackageDescription = desription,
                        PackagePath = path
                    }
                );
                db.SaveChanges();
            }
        }

        public ObservableCollection<Package> GetPackages()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<Package> packages = db.Packages.ToList();
                ObservableCollection<Package> res = new ObservableCollection<Package>(packages);

                return res;
            }
        }

        public ObservableCollection<Package> GetPackagesPagination(int page = 0, int len = 20)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<Package> packages = db.Packages.ToList().Slice(page, page + len);
                ObservableCollection<Package> res = new ObservableCollection<Package>(packages);

                return res;
            }

        }

        public void ApplyPackage(Package package)
        {
            PackageService packageService = new PackageService();
            packageService.setPackage(package);
        }

        public ObservableCollection<Package> sortByName(ObservableCollection<Package> packages, int mode)
        {
            if (mode == 1)
            {
                // Сортування по зростанню
                var sorted = packages.OrderBy(p => p.PackageName);
                return new ObservableCollection<Package>(sorted);
            }
            else if (mode == -1)
            {
                // Сортування по спаданню
                var sorted = packages.OrderByDescending(p => p.PackageName);
                return new ObservableCollection<Package>(sorted);
            }
            return packages;
        }

        public ObservableCollection<Package> findByName(string name)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var packages = db.Packages
                    .Where(p => p.PackageName.Contains(name))
                    .ToList();

                return new ObservableCollection<Package>(packages);
            }
        }


    }

}
