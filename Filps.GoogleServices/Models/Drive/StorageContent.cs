using System.Collections;
using System.Collections.Generic;
using Google.Apis.Drive.v3.Data;

namespace Filps.GoogleServices.Models.Drive
{
    public class StorageContent
    {
        public ICollection<File> Folders { get; set; } = new List<File>();
        public ICollection<File> Files { get; set; } = new List<File>();
    }
}