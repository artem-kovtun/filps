using System;
using Filps.Domain.Enums;

namespace Filps.Domain.Models.Files
{
    public class FileMetadata
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsPinned { get; set; }
        public Storage? Source { get; set; }
        public long? Size { get; set; }
        public DateTime? LastTimeAccessed { get; set; }
    }
}