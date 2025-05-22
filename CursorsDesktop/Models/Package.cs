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
        public int id {  get; set; }
        public string name { get; set; } = null;
        public string description { get; set; } = null;
        public string path { get; set; } = null;

        public ICollection<int> cursorIds{ get; set; } = new List<int>();

    }
}
