using Avalonia.Animation;
using CursorsDesktop.Clients;
using CursorsDesktop.Data;
using CursorsDesktop.DTO;
using CursorsDesktop.Entities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Cursor = CursorsDesktop.Entities.Cursor;


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

        public void downloadPackage(int id)
        {
            var packageService = new PackageService();
            var remotePackage = packageService.GetRemotePackageByIdAsync(id).GetAwaiter().GetResult();

            if (remotePackage == null)
            {
                Console.WriteLine("Package not found.");
                return;
            }

            string packageName = remotePackage.Name;
            string baseFolder = Path.Combine("Assets", "CursorPackages", packageName);
            Directory.CreateDirectory(baseFolder);

            using var httpClient = new HttpClient();

            foreach (var cursor in remotePackage.Cursors)
            {
                string fileName = $"{cursor.CursorName}.cur";
                string localPath = Path.Combine(baseFolder, fileName);

                try
                {
                    var bytes = httpClient.GetByteArrayAsync(cursor.pathToIcon).GetAwaiter().GetResult();
                    File.WriteAllBytes(localPath, bytes);
                    cursor.pathToIcon = localPath; // оновлюємо шлях локально для наступного кроку
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to download cursor '{cursor.CursorName}': {ex.Message}");
                }
            }

            // Після завантаження курсорів — додати все в БД
            packageService.ImportPackageToDatabase(remotePackage);
        }


        public async Task<PackageDTO> GetRemotePackageByIdAsync(int packageId)
        {
            var client = new PackageClient();
            return await client.GetPackageByIdAsync(packageId);
        }

        public async Task<ObservableCollection<PackageDTO>> GetAllPackagesAsync()
        {
            var client = new PackageClient();
            return await client.GetAllPackages();
        }

        public async Task<ObservableCollection<Package>> GetBrowsePackagesAsync()
        {
            var packageService = new PackageService();
            var remotePackages = await packageService.GetAllPackagesAsync();
            CursorService cursorService = new CursorService();

            ObservableCollection<Package> packages = new ObservableCollection<Package>();

            if (remotePackages == null)
            {
                Console.WriteLine("Package not found.");
                return packages;
            }

            foreach (var package in remotePackages)
            {
                var cursors = await cursorService.GetCursorsByPackageId(package.Id);
                packages.Add(new Package(package.Id, package.Name, package.Description, package.pathToIcon, cursors));
            }

            return packages;
        }



        public void ImportPackageToDatabase(PackageDTO remotePackage)
        {
            CursorService cursorService = new CursorService();
            CursorTypeService cursorTypeService = new CursorTypeService();

            string packageName = remotePackage.Name;
            string packagePath = Path.Combine("Assets", "CursorPackages", packageName);

            AddPackage(packageName, remotePackage.Description, packagePath);
            
            int newPackageId;
            using (var db = new ApplicationDbContext())
            {
                newPackageId = db.Packages.FirstOrDefault(p => p.PackageName == packageName)?.PackageId ?? 0;
            }

            if (newPackageId == 0)
            {
                Console.WriteLine("Package not inserted.");
                return;
            }

            foreach (var cursor in remotePackage.Cursors)
            {
                int typeId;
                using (var db = new ApplicationDbContext())
                {
                    var existingType = db.CursorTypes.FirstOrDefault(t => t.id == cursor.typeId);
                    typeId = existingType.id;
                    
                    //else
                    //{
                    //    cursorTypeService.AddCursorType(cursor.CursorType.Type);
                    //    typeId = db.CursorTypes.First(t => t.type == cursor.CursorType.Type).id;
                    //}
                }

                cursorService.AddCursor(cursor.CursorName, typeId, newPackageId, cursor.pathToIcon);
            }

            Console.WriteLine($"Package '{packageName}' added to DB.");
        }

    }

}
