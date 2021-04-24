using System.IO;

namespace Filps.GoogleServices.Models.Drive
{
    public class FileContent
    {
        public string Name { get; set; }
        public long? Size { get; set; }
        public MemoryStream Content { get; set; }
    }
}