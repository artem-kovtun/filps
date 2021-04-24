using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Filps.Common.Extensions;
using Filps.Domain.Models.Files;
using Microsoft.AspNetCore.Http;

namespace Filps.Mappings.Converters
{
    public class FilesConverter : IValueConverter<IEnumerable<IFormFile>, IEnumerable<FileData>>
    {
        public IEnumerable<FileData> Convert(IEnumerable<IFormFile> source, ResolutionContext context)
        {
            if (source == null) return null;
            
            return source.Select(file =>
            {
                var content = new MemoryStream();
                file.CopyTo(content);
                content.Position = 0;
                
                return new FileData(file.FileName.FileNameWithoutExtension(), file.FileName.GetExtension(), content.Length, content);
            }).ToList();

        }
    }
}