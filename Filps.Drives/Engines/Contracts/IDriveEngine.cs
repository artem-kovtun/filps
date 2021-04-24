using System.Threading.Tasks;
using Filps.GoogleDrive.Models;

namespace Filps.GoogleDrive.Engines.Contracts
{
    public interface IDriveEngine
    {
        Task<StorageData> GetStorageContentAsync(string parentId, string search);
        Task<FileData> DownloadFileAsync(string id);
    }
}