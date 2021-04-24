using System.Threading;
using System.Threading.Tasks;
using Filps.Application.DataStores;
using Filps.Application.Factories.Contracts;
using Filps.Common.Extensions;
using Filps.Domain.Models.Files;
using Filps.GoogleServices.Contacts;
using Google.Apis.Drive.v3;
using MediatR;

namespace Filps.Application.Requests.GoogleDrive.Queries.DownloadFile
{
    public class DownloadGoogleDriveFileQueryHandler : IRequestHandler<DownloadGoogleDriveFileQuery, FileData>
    {
        private static readonly string[] Scopes = { DriveService.Scope.Drive};
        
        private readonly IGoogleCredentialEngine _googleCredentialEngine;
        private readonly IGoogleDriveEngine _googleDriveEngine;
        private readonly IDataStoreFactory _dataStoreFactory;
        
        public DownloadGoogleDriveFileQueryHandler(IGoogleCredentialEngine googleCredentialEngine, IGoogleDriveEngine googleDriveEngine, IDataStoreFactory dataStoreFactory)
        {
            _googleCredentialEngine = googleCredentialEngine;
            _googleDriveEngine = googleDriveEngine;
            _dataStoreFactory = dataStoreFactory;
        }
        
        public async Task<FileData> Handle(DownloadGoogleDriveFileQuery request, CancellationToken cancellationToken)
        {
            var credential = await _googleCredentialEngine.GetCredentialAsync(Scopes,
                $"{request.Email ?? request.SessionId}//Google", cancellationToken, _dataStoreFactory.DataStore(request.SessionId, request.Email));
                
            var file = await _googleDriveEngine.DownloadFileAsync(credential, request.FileId);

            if (file == null) return null;
                
            return new FileData
            {
                Name = file.Name.FileNameWithoutExtension(),
                Extension = file.Name.GetExtension(),
                Content = file.Content
            };
                
        }
    }
}