using System.Collections.Generic;

namespace CursorsDesktop.Models
{
    public class CursorTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string SystemRole { get; set; } = null!;

        public ICollection<CursorModel> Cursors { get; set; } = new List<CursorModel>();
    }
}