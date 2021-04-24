using System;

namespace Filps.Application.Models.Storage
{
    public class StorageObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long? Size { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}