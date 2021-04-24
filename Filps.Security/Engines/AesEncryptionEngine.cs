using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Filps.Security.Contracts;
using Filps.Security.Models;
using Microsoft.Extensions.Options;

namespace Filps.Security.Engines
{
public class AesEncryptionEngine : IAesEncryptionEngine
    {
        private readonly KeyInformation _keyInformation;

        public AesEncryptionEngine(IOptions<KeyInformation> keyInformationOptions)
        {
            _keyInformation = keyInformationOptions.Value;
        }
        
        public async Task<string> EncryptAsync(string source)
        {
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = _keyInformation.Key;
                aesAlg.IV = _keyInformation.Iv;

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            await swEncrypt.WriteAsync(source);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }

        }

        public async Task<string> DecryptAsync(string decryptedSource)
        {
            var cipherBytes = Convert.FromBase64String(decryptedSource);
            
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = _keyInformation.Key;
                aesAlg.IV = _keyInformation.Iv;

                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (var msDecrypt = new MemoryStream(cipherBytes))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                             return await srDecrypt.ReadToEndAsync();
                        }
                    }
                }

            }
        }
    }
}