using System;

namespace Filps.Security.Models
{
    public class KeyInformation
    {
        public string Key { get; set; }
        public string Iv { get; set; }

        public byte[] KeyBytes => Convert.FromBase64String(Key);
        public byte[] IvBytes => Convert.FromBase64String(Iv);


    }
}