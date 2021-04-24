using System;
using System.IO;
using System.Threading.Tasks;

namespace Filps.Blob.Contracts
{
    public interface IBlobStorageEngine
    {
        Task<string> UploadAsync(string containerName, string fileName, Stream stream);
        Task<MemoryStream> DownloadAsync(string containerName, string filename);
        Task DeleteAsync(string containerName, string filename);
        Task<string> GetTemporaryDownloadUrl(string containerName, string filename, TimeSpan ttl);
    }
}