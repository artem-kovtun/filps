using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Filps.Application.Models.Files;
using Filps.Blob.Contracts;
using Filps.Common.Utilities;
using Filps.Domain.Enums;
using Filps.Domain.Repositories.Contracts;
using Filps.Security.Contracts;
using MediatR;

namespace Filps.Application.Requests.Files.Commands.SaveFile
{
    public class SaveFileCommandHandler : IRequestHandler<SaveFileCommand, string>
    {
        private readonly IFileRepository _repository;
        private readonly IBlobStorageEngine _blobStorageEngine;
        private readonly IAesEncryptionEngine _encryptionEngine;

        public SaveFileCommandHandler(IFileRepository repository, IBlobStorageEngine blobStorageEngine, IAesEncryptionEngine encryptionEngine)
        {
            _repository = repository;
            _blobStorageEngine = blobStorageEngine;
            _encryptionEngine = encryptionEngine;
        }

        public async Task<string> Handle(SaveFileCommand request, CancellationToken cancellationToken)
        {
            var interactiveDocument = new InteractiveDocument
            {
                Content = await _encryptionEngine.EncryptAsync(request.SerializedContent)
            };
            
            var xmlSerializer = new XmlSerializer(typeof(InteractiveDocument));
            var stream = new MemoryStream();
            xmlSerializer.Serialize(stream, interactiveDocument);
            stream.Position = 0;

            if (request.Id == null)
            {
                request.Id = StringUtilities.GetRandomStringKey();
            }

            await _blobStorageEngine.UploadAsync("files", request.Id, stream);
            await _repository.AddFileAsync(request.Id, request.Name, Storage.Filps, request.Email ?? "a.kvtn14@gmail.com");

            return request.Id;
        }
    }
}