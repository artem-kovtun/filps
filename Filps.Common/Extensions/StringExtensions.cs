using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Filps.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
        
        private static Dictionary<string, string> AvailableMimeTypes =>
            new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".xlsm", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };

        public static string MimeType(this string filename)
        {
            return AvailableMimeTypes[Path.GetExtension(filename).ToLowerInvariant()];
        }
        
        public static string FileNameWithoutExtension(this string filename)
        {
            return Path.GetFileNameWithoutExtension(filename);
        }

        public static string GetExtension(this string filename)
        {
            return Path.GetExtension(filename).Replace(".", string.Empty);
        }

        public static bool IsEmail(this string source)
        {
            return Regex.IsMatch(source,
                @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}