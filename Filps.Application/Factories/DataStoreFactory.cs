using Filps.Application.DataStores;
using Filps.Application.Factories.Contracts;
using Filps.Helpers.Engines.Contracts;
using Google.Apis.Util.Store;

namespace Filps.Application.Factories
{
    public class DataStoreFactory : IDataStoreFactory
    {
        private readonly ISessionEngine _sessionEngine;

        public DataStoreFactory(ISessionEngine sessionEngine)
        {
            _sessionEngine = sessionEngine;
        }

        public IDataStore DataStore(string sessionId, string email)
        {
            if (email == null)
            {
                return new SessionDataStore(_sessionEngine);
            }
            else
            {
                return new DatabaseDataStore();
            }
        }
    }
}