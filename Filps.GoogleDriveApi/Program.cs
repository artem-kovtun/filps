using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Filps.GoogleDriveApi
{
    class Program
    {
        static string[] Scopes = { DriveService.Scope.Drive};
        static string ApplicationName = "GoogleDriveAPIDemoApp";

        static void Main(string[] args)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("C:\\Users\\akvtn\\Projects\\Filps\\Filps.GoogleDriveApi\\client_secret.json", FileMode.Open, FileAccess.Read))
            {
                var credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-Demo");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        ClientId = "821014846315-smag2bqfaigu6vlqef51dnptvnrq8cd0.apps.googleusercontent.com",
                        ClientSecret = "9YnUHrleLzZxAGavEZnDNpip"
                    },
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
            
            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var request = service.Files.List();
            request.Q = "'me' in owners and (mimeType = 'application/vnd.google-apps.folder' or name contains '.idf') and '0B2LSqvF9qyjeSy1nd0EzZmphU28' in parents";

            var files = request.Execute().Files;
            foreach (var file in files)
            {
                Console.WriteLine(file.Name);
            }
        }
    }
}