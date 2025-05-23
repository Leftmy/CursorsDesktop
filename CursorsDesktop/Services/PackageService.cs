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


namespace CursorsDesktop.Services
{

    class PackageService
    {
        public PackageService()
        {

        }

        public Package getPackage(string name)
        {
            using(ApplicationDbContext db = new ApplicationDbContext())
            {
                Package package = db.Packages.Find(1);
                CursorType cursorType = db.CursorTypes.Find(1);
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

        public List<Package> ReadPackages()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<Package> packages = db.Packages.ToList();
                foreach (Package package in packages)
                {
                    Console.WriteLine(package.ToString());
                }
                return packages;
            }

        }

        public void ApplyPackage(Package package)
        {
            PackageService packageService = new PackageService();
            packageService.setPackage(package);
        }
    }

}
