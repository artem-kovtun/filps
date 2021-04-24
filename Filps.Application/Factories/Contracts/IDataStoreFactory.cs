using Google.Apis.Util.Store;

namespace Filps.Application.Factories.Contracts
{
    public interface IDataStoreFactory
    {
        public IDataStore DataStore(string sessionId, string email);
    }
}