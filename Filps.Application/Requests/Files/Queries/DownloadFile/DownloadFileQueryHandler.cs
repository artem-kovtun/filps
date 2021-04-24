using System.Threading;
using System.Threading.Tasks;
using Filps.Blob.Contracts;
using Filps.Domain.Models.Files;
using Filps.Domain.Repositories.Contracts;
using MediatR;

namespace Filps.Application.Requests.Files.Queries.DownloadFile
{
    public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, FileData>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IBlobStorageEngine _blobStorageEngine;

        public DownloadFileQueryHandler(IFileRepository fileRepository, IBlobStorageEngine blobStorageEngine)
        {
            _fileRepository = fileRepository;
            _blobStorageEngine = blobStorageEngine;
        }

        public async Task<FileData> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
        {
            var fileMetadata = await _fileRepository.GetFileAsync(request.FileId);
            var fileContentStream = await _blobStorageEngine.DownloadAsync("files", request.FileId);

            return new FileData
            {
                Name = fileMetadata.Name,
                Content = fileContentStream
            };
        }
    }
}