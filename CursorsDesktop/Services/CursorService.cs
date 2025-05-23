using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Xml.Linq;
using CursorsDesktop.Entities;
using Cursor = CursorsDesktop.Entities.Cursor;
using Avalonia.Diagnostics;
using CursorsDesktop.Data;

namespace CursorsDesktop.Services
{
    class CursorService
    {
        public List<int> getCursors(Package package)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<int> cursors = new List<int>();
                cursors = db.Packages.Find(package.PackageId).CursorIds.ToList();
                return cursors;
            }
               
        }

        public static void setCursor(int cursorId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (File.Exists(db.Cursors.Find(cursorId).CursorPath))
                {
                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Cursors", true))
                    {
                        string type = db.CursorTypes.Find(db.Cursors.Find(cursorId).CursorTypeId).type;
                        string path = db.Cursors.Find(cursorId).CursorPath;
                        key.SetValue(type, path);
                    }
                }
                else
                {
                    Console.WriteLine($"Файл не знайдено для курсора {db.Cursors.Find(cursorId).CursorName}: {db.Cursors.Find(cursorId).CursorPath}");
                }
            }
                
        }

    }
}
