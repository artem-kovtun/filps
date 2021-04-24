using System.Threading;
using System.Threading.Tasks;
using Filps.GoogleServices.Contacts;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;

namespace Filps.GoogleServices.Engines
{
    public class GoogleProfileEngine : IGoogleProfileEngine
    {
        public Task<Userinfo> GetUserInfoAsync(UserCredential credential)
        {
            var oauthService = new Oauth2Service(
                new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential
                });
                
            return oauthService.Userinfo.Get().ExecuteAsync(CancellationToken.None);
        }
    }
}