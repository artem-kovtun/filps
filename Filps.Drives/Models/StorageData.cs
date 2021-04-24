using System.Collections.Generic;
using Filps.GoogleDrive.Engines;

namespace Filps.GoogleDrive.Models
{
    public class StorageData
    {
        public ICollection<StorageObject> Folders { get; set; }
        public ICollection<StorageObject> Files { get; set; }
    }
}