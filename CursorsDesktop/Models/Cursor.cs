using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using CursorsDesktop.Services;

namespace CursorsDesktop.Entities
{
    [Index(nameof(CursorName), nameof(CursorTypeId), nameof(PackageId), IsUnique = true)]
    public class Cursor
    {
        [Key]
        public int CursorId { get; set; }

        // Зовнішній ключ до CursorType
        public int CursorTypeId { get; set; }
        [ForeignKey("CursorTypeId")]
        public CursorType CursorType { get; set; }

        // Зовнішній ключ до Package
        public int PackageId { get; set; }
        [ForeignKey("PackageId")]
        public Package Package { get; set; }

        public string CursorName { get; set; } = null;
        public string CursorPath { get; set; } = null;

    }
}
