namespace Filps.Application.Models.User
{
    public class StorageAuthentication
    {
        public Domain.Enums.Storage Storage { get; set; }
        public bool IsAuthorized { get; set; }
    }
}