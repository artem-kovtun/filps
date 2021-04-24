using System.Collections.Generic;

namespace Filps.Application.Models.Storage
{
    public class GetFilesResponse
    {
        public IList<StorageObject> Folders { get; set; } = new List<StorageObject>();
        public IList<StorageObject> Files { get; set; } = new List<StorageObject>();
    }
}