using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;

namespace Filps.GoogleServices.Contacts
{
    public interface IGoogleCredentialEngine
    {
        public Task<UserCredential> GetCredentialAsync(IEnumerable<string> scopes, string user, CancellationToken cancellationToken, IDataStore dataStore = null);
    }
}