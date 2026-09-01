namespace CursorsDesktop.Models
{
    public class CursorModel
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int PackageId { get; set; }
        public string Name { get; set; } = null!;
        public string PathToIcon { get; set; } = null!;
        public string PathToPreview { get; set; } = null!;
        
        public CursorTypeModel CursorType { get; set; } = null!;
        public PackageModel Package { get; set; } = null!;
    }
}