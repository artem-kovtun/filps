using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AutoMapper;
using Filps.Application.Constants;
using Filps.Application.Models.Files;
using Filps.Blob.Contracts;
using Filps.Domain.Repositories.Contracts;
using Filps.Security.Contracts;
using MediatR;
using File = Filps.Domain.Models.Files.File;

namespace Filps.Application.Requests.Files.Queries.GetFile
{
    public class GetFileQueryHandler : IRequestHandler<GetFileQuery, File>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IBlobStorageEngine _blobStorageEngine;
        private readonly IAesEncryptionEngine _aesEncryptionEngine;
        private readonly IMapper _mapper;

        public GetFileQueryHandler(IFileRepository fileRepository, IBlobStorageEngine blobStorageEngine, IMapper mapper, IAesEncryptionEngine aesEncryptionEngine)
        {
            _fileRepository = fileRepository;
            _blobStorageEngine = blobStorageEngine;
            _mapper = mapper;
            _aesEncryptionEngine = aesEncryptionEngine;
        }

        public async Task<File> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            var fileMetadata = await _fileRepository.GetFileAsync(request.Id);
            var fileContentStream = await _blobStorageEngine.DownloadAsync(BlobStorage.Containers.Files, request.Id);
            
            var xmlSerializer = new XmlSerializer(typeof(InteractiveDocument));
            var interactiveDocument = xmlSerializer.Deserialize(fileContentStream) as InteractiveDocument;

            var jsonSerializedContent = await _aesEncryptionEngine.DecryptAsync(interactiveDocument?.Content);

            return _mapper.Map(fileMetadata, new File {SerializedContent = jsonSerializedContent});
        }
    }
}