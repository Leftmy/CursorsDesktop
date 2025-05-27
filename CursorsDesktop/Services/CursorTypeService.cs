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
    class CursorTypeService
    {
        public void AddCursorType(string type)
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

        public List<CursorType> ReadCursorTypes()
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
