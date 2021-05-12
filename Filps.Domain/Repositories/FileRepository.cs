using System.Threading.Tasks;
using Filps.Domain.Configurations;
using Filps.Domain.Constants;
using Filps.Domain.Models.Files;
using Filps.Domain.Models.Shared;
using Filps.Domain.Repositories.Contracts;
using Microsoft.Extensions.Options;

namespace Filps.Domain.Repositories
{
    public class FileRepository: Repository, IFileRepository
    {
        public FileRepository(IOptions<DatabaseConfiguration> databaseConfiguration) : base(databaseConfiguration) { }

        public Task SaveFileAsync(string id, string name, string userEmail)
        {
            return ExecuteScalarProcedure<string>(StoredProcedures.SaveFile, new
            {
                id,
                name,
                userEmail
            });
        }

        public Task<FileMetadata> GetFileAsync(string id)
        {
            return ExecuteJsonResultProcedureAsync<FileMetadata>(StoredProcedures.GetFile, new
            {
                id
            });
        }

        public Task<PagedList<FileMetadata>> GetUserFilesAsync(GetFilesFilters filters)
        {
            return ExecuteJsonResultProcedureAsync<PagedList<FileMetadata>>(StoredProcedures.GetFiles, new
            {
                userEmail = filters.Email,
                page = filters.Page ?? 1,
                take = filters.Take ?? 10,
                search = filters.Search ?? string.Empty
            });
        }

        public async Task ToggleFilePinAsync(string id)
        { 
            await ExecuteScalarProcedure<string>(StoredProcedures.ToggleFilePin, new
            {
                id
            });
        }

        public Task<bool> DeleteFile(string id)
        {
            return ExecuteScalarProcedure<bool>(StoredProcedures.DeleteFile, new
            {
                id
            });
        }
    }
}