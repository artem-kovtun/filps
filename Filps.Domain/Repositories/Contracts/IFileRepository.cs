using System.Threading.Tasks;
using Filps.Domain.Models.Files;
using Filps.Domain.Models.Shared;

namespace Filps.Domain.Repositories.Contracts
{
    public interface IFileRepository
    {
        Task SaveFileAsync(string id, string name, string userEmail);
        Task<FileMetadata> GetFileAsync(string id);
        Task<PagedList<FileMetadata>> GetUserFilesAsync(GetFilesFilters filters);
        Task ToggleFilePinAsync(string id);
        Task<bool> DeleteFile(string id);
    }
}