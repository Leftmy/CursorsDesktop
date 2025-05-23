using Avalonia;
using System;
using CursorsDesktop.Data;
using Microsoft.EntityFrameworkCore;
using CursorsDesktop.Entities;
using CursorsDesktop.Services;
using CursorsDesktop.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using Tmds.DBus.Protocol;

namespace CursorsDesktop
{
    internal sealed class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {

            BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
            AddCursorType("Arrow");
            AddPackage("MaterialBlack", "CursorsDesktop", "C:\\Users\\dariy\\Source\\Repos\\CursorsDesktop\\CursorsDesktop\\Assets\\Cursors\\MaterialBlack");
            AddCursor("MaterialBlack_Arrow", 1, 1, "C:\\Users\\dariy\\Source\\Repos\\CursorsDesktop\\CursorsDesktop\\Assets\\Cursors\\MaterialBlack\\pointer.cur");

            PackageService packageService = new PackageService();
            Package package = packageService.getPackage("MaterialBlack");
            packageService.setPackage(package);

        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();


        public static void AddCursorType(string type)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.CursorTypes.Add(
                    new CursorType()
                    {
                        type = type
                    }
                );
                db.SaveChanges();
            }
        }
        public static void AddPackage(string name, string desription, string path)
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

        public static void AddCursor(string name, int cursorTypeId/*, CursorType cursorType*/, int packageId/*, Package package*/, string path)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                Cursor cursor =
                    new Cursor()
                    {
                        CursorName = name,
                        CursorTypeId = cursorTypeId,
                        CursorType = db.CursorTypes.Find(cursorTypeId),
                        PackageId = packageId,
                        Package = db.Packages.Find(packageId),
                        CursorPath = path
                    };
                
                db.Cursors.Add(
                    cursor
                );

                db.SaveChanges();
                CursorType foundType = db.CursorTypes.Find(cursorTypeId);
                foundType.CursorIds.Add(cursor.CursorId);
                Package foundPackage = db.Packages.Find(packageId);
                foundPackage.CursorIds.Add(cursor.CursorId);
                db.SaveChanges();
            }
        }

        public static List<Cursor> ReadCursors()
        {
            using (  ApplicationDbContext db = new ApplicationDbContext())
            {
                List<Cursor> cursors = db.Cursors.ToList();
                foreach ( Cursor cursor in cursors )
                {
                    Console.WriteLine(cursor.ToString());
                }
                return cursors;
            }

        }

        public static List<Package> ReadPackages()
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

        public static List<CursorType> ReadCursorTypes()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<CursorType> cursorTypes = db.CursorTypes.ToList();
                foreach (CursorType cursorType in cursorTypes)
                {
                    Console.WriteLine(cursorType.ToString());
                }
                return cursorTypes;
            }

        }

        public static void ApplyPackage(Package package)
        {
            PackageService packageService = new PackageService();
            packageService.setPackage(package);
        }


    }
}
