using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursorsDesktop.DTO
{
    public class PackageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string pathToIcon { get; set; }
        public List<CursorDTO> Cursors { get; set; }
    }
}
