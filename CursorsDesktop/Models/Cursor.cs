using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CursorsDesktop.Entities
{
    public class Cursor
    {
        [Key]
        public int id { get; set; }

        // Зовнішній ключ до CursorType
        public int CursorTypeId { get; set; }
        [ForeignKey("CursorTypeId")]
        public CursorType CursorType { get; set; }

        // Зовнішній ключ до Package
        public int PackageId { get; set; }
        [ForeignKey("PackageId")]
        public Package Package { get; set; }

        public string name { get; set; } = null;
        public string path { get; set; } = null;
    }
}
