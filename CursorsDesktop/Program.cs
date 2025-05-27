using Avalonia;
using System;
using CursorsDesktop.Data;
using CursorsDesktop.Clients;
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

            PackageService packageService = new PackageService();
            CursorService cursorService = new CursorService();

            BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

            


            //AddCursorType("Arrow");
            //AddCursorType("Wait");
            //AddCursorType("Help");
            //AddCursorType("AppStarting");
            //AddCursorType("Crosshair");
            //AddCursorType("IBeam");
            //AddCursorType("NWPen");
            //AddCursorType("No");
            //AddCursorType("SizeNS");
            //AddCursorType("SizeWE");
            //AddCursorType("SizeNWSE");
            //AddCursorType("SizeNESW");
            //AddCursorType("SizeAll");
            //AddCursorType("UpArrow");
            //AddCursorType("Hand");
            //AddCursorType("Pin");
            //AddCursorType("Person");
            //AddCursorType("None");
            //packageService.AddPackage("MaterialBlack2", "CursorsDesktop", "C:\\Users\\5\\Desktop\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\MaterialBlack\\");
            ////packageService.AddPackage("MaterialBlack2", "CursorsDesktop", "C:\\Users\\5\\Desktop\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\MaterialBlack\\");
            //cursorService.AddCursor("MatBlack_Arrow", 1, 1, "C:\\Users\\dariy\\source\\repos\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\MaterialBlack\\pointer.cur");
            //cursorService.AddCursor("MatBlack_Arrow", 2, 1, "C:\\Users\\dariy\\source\\repos\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\MaterialBlack\\pointer.cur");
            //cursorService.AddCursor("MatBlack_Arrow", 3, 1, "C:\\Users\\dariy\\source\\repos\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\MaterialBlack\\pointer.cur");
            //cursorService.AddCursor("MatBlack_Arrow", 4, 1, "C:\\Users\\dariy\\source\\repos\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\MaterialBlack\\pointer.cur");
            //cursorService.AddCursor("MatBlack_Arrow", 5, 1, "C:\\Users\\dariy\\source\\repos\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\MaterialBlack\\pointer.cur");
            //cursorService.AddCursor("MatBlack_Arrow", 6, 1, "C:\\Users\\dariy\\source\\repos\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\MaterialBlack\\pointer.cur");
            //cursorService.AddCursor("MatBlack_Arrow", 7, 1, "C:\\Users\\dariy\\source\\repos\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\MaterialBlack\\pointer.cur");
            //cursorService.AddCursor("MatBlack_Arrow", 8, 1, "C:\\Users\\dariy\\source\\repos\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\MaterialBlack\\pointer.cur");
            //cursorService.AddCursor("MatBlack_Arrow", 9, 1, "C:\\Users\\dariy\\source\\repos\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\MaterialBlack\\pointer.cur");
            //cursorService.AddCursor("MatBlack_Arrow", 10, 1, "C:\\Users\\dariy\\source\\repos\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\MaterialBlack\\pointer.cur");
            //cursorService.AddCursor("MatBlack_Arrow", 11, 1, "C:\\Users\\dariy\\source\\repos\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\MaterialBlack\\pointer.cur");
            //cursorService.AddCursor("MatBlack_Arrow", 12, 1, "C:\\Users\\dariy\\source\\repos\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\MaterialBlack\\pointer.cur");
            //cursorService.AddCursor("MatBlack_Arrow", 13, 1, "C:\\Users\\dariy\\source\\repos\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\MaterialBlack\\pointer.cur");
            //cursorService.AddCursor("MatBlack_Arrow", 14, 1, "C:\\Users\\dariy\\source\\repos\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\MaterialBlack\\pointer.cur");
            //cursorService.AddCursor("MatBlack_Arrow", 15, 1, "C:\\Users\\dariy\\source\\repos\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\MaterialBlack\\pointer.cur");
            //cursorService.AddCursor("MatBlack_Arrow", 16, 1, "C:\\Users\\dariy\\source\\repos\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\MaterialBlack\\pointer.cur");
            //cursorService.AddCursor("MatBlack_Arrow", 17, 1, "C:\\Users\\dariy\\source\\repos\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\MaterialBlack\\pointer.cur");

            //packageService.AddPackage("BPackage", "CursorsDesktop", "C:\\Users\\5\\Desktop\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\test\\");
            //cursorService.AddCursor("Blink_Cursor", 1, 4, "C:\\Users\\5\\Desktop\\CursorsDesktop\\CursorsDesktop\\Assets\\CursorPackages\\test\\blinkDagger.cur");

            //Package package = packageService.getPackage("MaterialBlack2");
            //packageService.setPackage(package);


        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();


        




    }
}
