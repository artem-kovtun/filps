﻿using System;
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
            using (var aes = new AesManaged())
            {
                var encryptor = aes.CreateEncryptor(_keyInformation.KeyBytes, _keyInformation.IvBytes);
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

            string plaintext;

            using (var aes = new AesManaged())
            {
                var decryptor = aes.CreateDecryptor(_keyInformation.KeyBytes, _keyInformation.IvBytes);
                using (var msDecrypt = new MemoryStream(cipherBytes))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = await srDecrypt.ReadToEndAsync();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}