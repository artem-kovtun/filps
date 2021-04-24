namespace Filps.Blob.Models
{
    public class BlobConfiguration
    {
        public string ConnectionString { get; set; }
        public int ParallelOperationThreadCount { get; } = 1;
        public int SingleBlobUploadThresholdInBytes { get; } = 157286400; // 150 MB
    }
}