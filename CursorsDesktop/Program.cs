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
            PackageService packageService = new PackageService();
            CursorService cursorService = new CursorService();
            AddCursorType("Arrow");
            packageService.AddPackage("MaterialBlack", "CursorsDesktop", "C:\\Users\\5\\Desktop\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\MaterialBlack\\");
            cursorService.AddCursor("MaterialBlack_Arrow", 1, 1, "C:\\Users\\5\\Desktop\\CursorsDesktop\\CursorsDesktop\\Assets\\Cursors\\MaterialBlack\\pointer.cur");

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




    }
}
