using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Filps.GoogleServices.Contacts;
using Filps.GoogleServices.Models.Drive;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Http;
using Google.Apis.Services;

namespace Filps.GoogleServices.Engines
{
    public class GoogleDriveEngine : IGoogleDriveEngine
    {
        private const string GoogleDriveFolderMimeType = "application/vnd.google-apps.folder";
        
        public async Task<StorageContent> GetMyDriveContentAsync(GoogleCredential credential, StorageFilters filters)
        {
            var baseQuery = $"'me' in owners and trashed = false and '{filters.ParentId ?? "root"}' in parents and name contains '{filters.Search}'";
            var service = DriveService(credential);
            
            var r = service.Files.List();
            r.Q = $"{baseQuery} and (mimeType = '{GoogleDriveFolderMimeType}' or name contains '.idf')";

            var files = (await r.ExecuteAsync(CancellationToken.None)).Files;
                
            var response = new StorageContent();

            foreach (var file in files)
            {
                if (file.MimeType == GoogleDriveFolderMimeType)
                {
                    response.Folders.Add(file);
                }
                else
                {
                    response.Files.Add(file);
                }
            }

            return response;
        }

        public async Task<FileContent> DownloadFileAsync(GoogleCredential credential, string id)
        {
            var service = DriveService(credential);
            
            var getRequest = service.Files.Get(id);
            var file = await getRequest.ExecuteAsync(CancellationToken.None);

            if (file == null) return null;
            
            var stream = new MemoryStream();
            await getRequest.DownloadAsync(stream, CancellationToken.None);
            
            stream.Position = 0;
            return new FileContent
            {
                Name = file.Name,
                Content = stream
            };
        }
        
        private static DriveService DriveService(IConfigurableHttpClientInitializer credential)
        {
            return new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential
            });
        }
    }
}