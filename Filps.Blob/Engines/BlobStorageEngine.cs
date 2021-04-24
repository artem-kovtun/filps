using System;
using System.IO;
using System.Threading.Tasks;
using Filps.Blob.Contracts;
using Filps.Blob.Models;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Filps.Blob.Engines
{
    public class BlobStorageEngine : IBlobStorageEngine
    {
        private readonly BlobConfiguration _options;
        private readonly CloudStorageAccount _storageAccount;

        public BlobStorageEngine(IOptions<BlobConfiguration> configurationOptions)
        {
            _options = configurationOptions.Value;
            _storageAccount = CloudStorageAccount.Parse(_options.ConnectionString);
        }

        public async Task<string> UploadAsync(string containerName, string fileName, Stream stream)
        {
            var blockBlob = await GetCloudBlockBlob(containerName, fileName);

            if (stream.CanSeek && stream.CanRead && stream.Position > 0)
                stream.Seek(0L, SeekOrigin.Begin);

            await blockBlob.UploadFromStreamAsync(stream);

            return $"https://dfilpsstorage.blob.core.windows.net/{containerName}/{fileName}";
        }

        public async Task<MemoryStream> DownloadAsync(string containerName, string filename)
        {
            var blockBlob = await GetCloudBlockBlob(containerName, filename);
            var memoryStream = new MemoryStream();
            await blockBlob.DownloadToStreamAsync(memoryStream);

            if (memoryStream.CanSeek && memoryStream.Position > 0)
                memoryStream.Seek(0L, SeekOrigin.Begin);

            return memoryStream;
        }

        public async Task DeleteAsync(string containerName, string filename)
        {
            var blockBlob = await GetCloudBlockBlob(containerName, filename);
            await blockBlob.DeleteIfExistsAsync();
        }

        public async Task<string> GetTemporaryDownloadUrl(string containerName, string filename, TimeSpan ttl)
        {
            var blockBlob = await GetCloudBlockBlob(containerName, filename);
            return GetContainerSharedAccessUri(blockBlob, ttl);
        }

        private async Task<CloudBlockBlob> GetCloudBlockBlob(string containerName, string fileName, string contentType = null)
        {
            var blobClient = CreateCloudBlobClient(_storageAccount);
            var container = await CreateContainerIfNotExistsAsync(blobClient, containerName);

            var blockBlob = container.GetBlockBlobReference(fileName);

            if (!string.IsNullOrEmpty(contentType))
                blockBlob.Properties.ContentType = contentType;

            return blockBlob;
        }
        
        private static async Task<CloudBlobContainer> CreateContainerIfNotExistsAsync(CloudBlobClient blobClient, string containerName)
        {
            var  container = blobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();
            return container;
        }

        private CloudBlobClient CreateCloudBlobClient(CloudStorageAccount storageAccount)
        {
            var options = new BlobRequestOptions
            {
                ParallelOperationThreadCount = _options.ParallelOperationThreadCount,
                SingleBlobUploadThresholdInBytes = _options.SingleBlobUploadThresholdInBytes
            };
            var blobClient = storageAccount.CreateCloudBlobClient();
            blobClient.DefaultRequestOptions = options;

            return blobClient;
        }

        private static string GetContainerSharedAccessUri(CloudBlockBlob blob, TimeSpan ttl)
        {
            if (blob == null)
            {
                throw new ArgumentNullException(nameof(blob));
            }
            var sharedAccessBlobPolicy = new SharedAccessBlobPolicy
            {
                SharedAccessExpiryTime = DateTime.Now.Add(ttl), 
                Permissions = SharedAccessBlobPermissions.Read
            };

            return $"{blob.Uri}{blob.GetSharedAccessSignature(sharedAccessBlobPolicy)}";
        }
    }
}