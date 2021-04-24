using System.Threading.Tasks;
using Filps.GoogleServices.Models.Drive;
using Google.Apis.Auth.OAuth2;

namespace Filps.GoogleServices.Contacts
{
    public interface IGoogleDriveEngine
    {
        public Task<StorageContent> GetMyDriveContentAsync(UserCredential credential, StorageFilters filters);
        public Task<FileContent> DownloadFileAsync(UserCredential credential, string id);
        
    }
}