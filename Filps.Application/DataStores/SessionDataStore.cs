using System.Threading.Tasks;
using Filps.Helpers.Engines.Contracts;
using Google.Apis.Util.Store;

namespace Filps.Application.DataStores
{
    public class SessionDataStore : IDataStore
    {
        private readonly ISessionEngine _sessionEngine;

        public SessionDataStore(ISessionEngine sessionEngine)
        {
            _sessionEngine = sessionEngine;
        }
        
        public Task StoreAsync<T>(string key, T value)
        {
            return _sessionEngine.StoreAsync(key, value);
        }

        public Task DeleteAsync<T>(string key)
        {
            return _sessionEngine.DeleteAsync(key);
        }

        public Task<T> GetAsync<T>(string key)
        {
            return _sessionEngine.GetAsync<T>(key);

        }

        public Task ClearAsync()
        {
            return _sessionEngine.ClearAsync();
        }
    }
}