using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2.Data;

namespace Filps.GoogleServices.Contacts
{
    public interface IGoogleProfileEngine
    {
        public Task<Userinfo> GetUserInfoAsync(UserCredential credential);
    }
}