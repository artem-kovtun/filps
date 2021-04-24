using System.Collections;
using System.Collections.Generic;

namespace Filps.Application.Models.User
{
    public class UserAuthentication
    {
        public IEnumerable<StorageAuthentication> Storages { get; set; }
    }
}