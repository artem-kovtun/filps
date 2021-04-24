using System.Threading.Tasks;
using Filps.Domain.Enums;
using Filps.Domain.Models.Files;

namespace Filps.Domain.Repositories.Contracts
{
    public interface IFileRepository
    {
        Task AddFileAsync(string id, string name, Storage storage, string createdBy);
        Task<FileMetadata> GetFileAsync(string id);
        Task<FileMetadata> GetUserFilesAsync(string email);
    }
}