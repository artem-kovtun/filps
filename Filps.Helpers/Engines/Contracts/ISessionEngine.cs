using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Filps.Helpers.Engines.Contracts
{
    public interface ISessionEngine
    {
        public ISession Session { get; set; }
        public Task StoreAsync<T>(string key, T value);
        public Task<T> GetAsync<T>(string key);
        public Task DeleteAsync(string key);
        public Task ClearAsync();
    }
}