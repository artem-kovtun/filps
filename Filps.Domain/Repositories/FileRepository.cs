using System;
using System.Threading.Tasks;
using Filps.Domain.Configurations;
using Filps.Domain.Constants;
using Filps.Domain.Enums;
using Filps.Domain.Models.Files;
using Filps.Domain.Repositories.Contracts;
using Microsoft.Extensions.Options;

namespace Filps.Domain.Repositories
{
    public class FileRepository: Repository, IFileRepository
    {
        public FileRepository(IOptions<DatabaseConfiguration> databaseConfiguration) : base(databaseConfiguration) { }

        public Task AddFileAsync(string id, string name, Storage storage, string createdBy)
        {
            return QueryFirstOrDefaultAsync<string>(StoredProcedures.AddFile, new
            {
                _id = id,
                _name = name,
                _storage = storage,
                _created_by = createdBy
            });
        }

        public Task<FileMetadata> GetFileAsync(string id)
        {
            return QueryJsonAsync<FileMetadata>(StoredProcedures.GetFile, new
            {
                _id = id
            });
        }

        public Task<FileMetadata> GetUserFilesAsync(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}