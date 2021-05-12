using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AutoMapper;
using Filps.Application.Constants;
using Filps.Application.Models.Files;
using Filps.Blob.Contracts;
using Filps.Common.Utilities;
using Filps.Domain.Models.Files;
using Filps.Domain.Repositories.Contracts;
using Filps.Security.Contracts;
using MediatR;
using Newtonsoft.Json;

namespace Filps.Application.Requests.Files.Queries.OpenFile
{
    public class OpenFileQueryHandler : IRequestHandler<OpenFileQuery, File>
    {
        private readonly IFileRepository _repository;
        private readonly IBlobStorageEngine _blobStorageEngine;
        private readonly IAesEncryptionEngine _aesEncryptionEngine;

        public OpenFileQueryHandler(IAesEncryptionEngine aesEncryptionEngine, IFileRepository repository, IBlobStorageEngine blobStorageEngine)
        {
            _aesEncryptionEngine = aesEncryptionEngine;
            _repository = repository;
            _blobStorageEngine = blobStorageEngine;
        }

        public async Task<File> Handle(OpenFileQuery request, CancellationToken cancellationToken)
        {
            
            var xmlSerializer = new XmlSerializer(typeof(InteractiveDocument));
            var interactiveDocument = xmlSerializer.Deserialize(request.File.Content) as InteractiveDocument;

            var jsonSerializedContent = await _aesEncryptionEngine.DecryptAsync(interactiveDocument?.Content);

            var file = new File
            {
                Name = request.File.Name,
                Size = request.File.Size,        
                SerializedContent = jsonSerializedContent
            };

            if (request.Email != null)
            {
                file.Id = StringUtilities.GetRandomStringKey();
                await _repository.SaveFileAsync(file.Id, file.Name, request.Email);
                await _blobStorageEngine.UploadAsync(BlobStorage.Containers.Files, file.Id, request.File.Content);
            }
            
            return file;
        }
    }
}