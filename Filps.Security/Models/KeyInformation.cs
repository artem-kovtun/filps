using System;
using System.Security.Cryptography;

namespace Filps.Security.Models
{
    public class KeyInformation
    {
        public byte[] Key { get; }
        public byte[] Iv { get; }

        public string KeyString => Convert.ToBase64String(Key);
        public string IvString => Convert.ToBase64String(Iv);

        public KeyInformation()
        {
            using var myAes = Aes.Create();
            if (myAes != null)
            {
                Key = myAes.Key;
                Iv = myAes.IV;
            }
        }
        public KeyInformation(byte[] key, byte[] iv)
        {
            Key = key;
            Iv = iv;
        }
        public KeyInformation(string key, string iv)
        {
            Key = Convert.FromBase64String(key);
            Iv = Convert.FromBase64String(iv);
        }
    }
}