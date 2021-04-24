using System.Threading.Tasks;
using Google.Apis.Util.Store;

namespace Filps.GoogleDriveApi
{
    public class CustomDataStore : IDataStore
    {
        public async Task StoreAsync<T>(string key, T value)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAsync<T>(string key)
        {
            throw new System.NotImplementedException();
        }

        public async Task<T> GetAsync<T>(string key)
        {
            throw new System.NotImplementedException();
        }

        public async Task ClearAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}