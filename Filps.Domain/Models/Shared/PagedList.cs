using System.Collections.Generic;

namespace Filps.Domain.Models.Shared
{
    public class PagedList<T>
    {
        public long? Total { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}