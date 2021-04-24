using System.Threading.Tasks;

namespace Filps.Security.Contracts
{
    public interface IAesEncryptionEngine
    {
        Task<string> EncryptAsync(string source);

        Task<string> DecryptAsync(string decryptedSource);
    }
}