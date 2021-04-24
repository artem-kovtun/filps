using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Filps.GoogleDrive.Engines.Contracts;
using Filps.GoogleDrive.Models;
using Filps.Helpers.Engines.Contracts;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.Extensions.Options;

namespace Filps.GoogleDrive.Engines
{
    public class GoogleDriveEngine : IGoogleDriveEngine
    {
        private static readonly string[] Scopes = { DriveService.Scope.Drive};
        private readonly GoogleCredentials _credentials;
        private readonly ISessionEngine _sessionEngine;
        
        public GoogleDriveEngine(IOptions<GoogleCredentials> googleCredentialsOptions, ISessionEngine sessionEngine)
        {
            _sessionEngine = sessionEngine;
            _credentials = googleCredentialsOptions.Value;
        }
        
        public async Task<StorageData> GetStorageContentAsync(string parentId, string search)
        {
            var service = await GetGoogleDriveServiceAsync();
            
            var listRequest = service.Files.List();
            listRequest.Q = $"'me' in owners and (mimeType = 'application/vnd.google-apps.folder' or name contains '.idf') and '{parentId ?? "root"}' in parents and trashed = false";

            var files = (await listRequest.ExecuteAsync(CancellationToken.None)).Files;
                
            var response = new StorageData();
            
            foreach (var file in files)
            {
                if (file.MimeType == "application/vnd.google-apps.folder")
                {
                    response.Folders.Add(new StorageObject
                    {
                        Id = file.Id,
                        Name = file.Name,
                        CreatedOn = file.CreatedTime
                    });
                }
                else
                {
                    response.Files.Add(new StorageObject
                    {
                        Id = file.Id,
                        Name = file.Name,
                        CreatedOn = file.CreatedTime
                    });
                }
            }

            return response;
        }

        public async Task<FileData> DownloadFileAsync(string id)
        {
            var service = await GetGoogleDriveServiceAsync();
            
            var getRequest = service.Files.Get(id);
            var file = await getRequest.ExecuteAsync(CancellationToken.None);
            var stream = new MemoryStream();
            await getRequest.DownloadAsync(stream, CancellationToken.None);
            
            stream.Position = 0;
            return new FileData
            {
                Name = file.Name,
                Content = stream
            };
        }

        private async Task<DriveService> GetGoogleDriveServiceAsync()
        {
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = _credentials.ClientId,
                    ClientSecret = _credentials.ClientSecret
                }, 
                Scopes,
                $"{_sessionEngine.Session.Id}//Google",
                CancellationToken.None);
                
            return new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential
            });
        }
    }
}