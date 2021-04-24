using System.IO;

namespace Filps.GoogleDrive.Models
{
    public class FileData
    {
        public string Name { get; set; }
        public long? Size { get; set; }
        public MemoryStream Content { get; set; }
    }
}