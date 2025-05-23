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
    public class Package
    {
        [Key]
        public int PackageId {  get; set; }
        public string PackageName { get; set; } = null;
        public string PackageDescription { get; set; } = null;
        public string PackagePath { get; set; } = null;

        public ICollection<int> CursorIds{ get; set; } = new List<int>();

    }
}
