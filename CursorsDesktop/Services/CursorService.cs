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
using System.Collections.ObjectModel;

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
        public void AddCursor(string name, int cursorTypeId/*, CursorType cursorType*/, int packageId/*, Package package*/, string path)
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

        public ObservableCollection<Cursor> getCursors()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<Cursor> cursors = db.Cursors.ToList();
                ObservableCollection<Cursor> res = new ObservableCollection<Cursor>(cursors);

                return res;
            }

        }

        public ObservableCollection<Cursor> sortByName(ObservableCollection<Cursor> cursors, int mode)
        {
            if (mode == 1)
            {
                // Сортування по зростанню
                var sorted = cursors.OrderBy(c => c.CursorName);
                return new ObservableCollection<Cursor>(sorted);
            }
            else if (mode == -1)
            {
                // Сортування по спаданню
                var sorted = cursors.OrderByDescending(c => c.CursorName);
                return new ObservableCollection<Cursor>(sorted);
            }
            return cursors;
        }

        public ObservableCollection<Cursor> findByName(string name)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var cursors = db.Cursors
                    .Where(c => c.CursorName.Contains(name))
                    .ToList();

                return new ObservableCollection<Cursor>(cursors);
            }
        }

    }
}
