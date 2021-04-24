using System;
using System.Text;
using System.Threading.Tasks;
using Filps.Helpers.Engines.Contracts;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Filps.Helpers.Engines
{
    public class SessionEngine : ISessionEngine
    {
        public ISession Session  { get; set; }

        public SessionEngine(IHttpContextAccessor httpContextAccessor)
        {
            Session = httpContextAccessor?.HttpContext?.Session;
        }

        public Task StoreAsync<T>(string key, T value)
        {
            var json = JsonConvert.SerializeObject(value);
            Session.Set(key, Encoding.UTF8.GetBytes(json));
            
            return Task.CompletedTask;
        }

        public  Task<T> GetAsync<T>(string key)
        {
            var tcs = new TaskCompletionSource<T>();
            
            Session.TryGetValue(key, out var json);
            if (json != null)
            {
                try
                {
                    tcs.SetResult(JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(json)));
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            }
            else
            {
                tcs.SetResult(default);
            }
            return tcs.Task;
        }

        public  Task DeleteAsync(string key)
        {
            Session.Remove(key);
            return Task.CompletedTask;
        }

        public Task ClearAsync()
        {
            Session.Clear();
            return Task.CompletedTask;
        }
    }
}