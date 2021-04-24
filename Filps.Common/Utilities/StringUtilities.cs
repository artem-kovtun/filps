using System;
using System.Linq;

namespace Filps.Common.Utilities
{
    public class StringUtilities
    {
        private static readonly Random Random = new Random();
        
        public static string GetRandomStringKey(int length = 32)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}