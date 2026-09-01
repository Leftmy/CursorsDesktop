using System;
using System.Collections.Generic;

namespace CursorsDesktop.Models
{
    public class PackageModel
    {
        public int Id { get; set; }
        public string PackageName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PathToPreview { get; set; } = null!;
        public DateTime DateCreated { get; set; }

        public ICollection<CursorModel> Cursors { get; set; } = new List<CursorModel>();
        
    }
}