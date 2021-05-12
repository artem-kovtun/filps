using System.Threading.Tasks;
using Filps.GoogleServices.Models.Drive;
using Google.Apis.Auth.OAuth2;

namespace Filps.GoogleServices.Contacts
{
    public interface IGoogleDriveEngine
    {
        public Task<StorageContent> GetMyDriveContentAsync(GoogleCredential credential, StorageFilters filters);
        public Task<FileContent> DownloadFileAsync(GoogleCredential credential, string id);
        
    }
}