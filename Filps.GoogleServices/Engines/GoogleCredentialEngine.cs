using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Filps.GoogleServices.Contacts;
using Filps.GoogleServices.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Options;

namespace Filps.GoogleServices.Engines
{
    public class GoogleCredentialEngine : IGoogleCredentialEngine
    {
        private readonly GoogleCredentials _credentials;
        
        public GoogleCredentialEngine(IOptions<GoogleCredentials> googleUserCredentialOptions)
        {
            _credentials = googleUserCredentialOptions.Value;
        }
        
        public async Task<UserCredential> GetCredentialAsync(IEnumerable<string> scopes, string user,  CancellationToken cancellationToken, IDataStore dataStore = null)
        {
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = _credentials.ClientId,
                    ClientSecret = _credentials.ClientSecret
                },
                scopes,
                user,
                cancellationToken, 
                dataStore);
            
            if (credential.Token.IsExpired(credential.Flow.Clock))
            {
                await credential.RefreshTokenAsync(CancellationToken.None);
            }
            
            return credential;
        }
        
    }
}