using System.IO;

namespace Filps.Domain.Models.Files
{
    public class FileData
    {
        public FileData() {}
        public FileData(string name, string extension, long? size, MemoryStream content)
        {
            Name = name;
            Extension = extension;
            Content = content;
        }
        
        public string Name { get; set; }
        public string Extension { get; set; }
        public long? Size { get; set; }
        public MemoryStream Content { get; set; }
    }
}