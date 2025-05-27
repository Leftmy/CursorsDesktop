using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CursorsDesktop.Entities
{
    [Index(nameof(PackageName), IsUnique = true)]
    public class Package
    {
        [Key]
        public int PackageId {  get; set; }
        public string PackageName { get; set; }
        public string PackageDescription { get; set; } = null;
        public string PackagePath { get; set; } = null;

        public ICollection<int> CursorIds{ get; set; } = new List<int>();

        public Package(int packageId, string packageName, string packageDescription, string packagePath, ICollection<int> cursorIds)
        {
            PackageId = packageId;
            PackageName = packageName;
            PackageDescription = packageDescription;
            PackagePath = packagePath;
            CursorIds = cursorIds;
        }
        public Package() { }
    }
}
