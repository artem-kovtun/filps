using System.IO;
using AutoMapper;
using Filps.Common.Extensions;
using Filps.Domain.Models.Files;
using Microsoft.AspNetCore.Http;

namespace Filps.Mappings.Converters
{
    public class FileConverter : IValueConverter<FormFile, FileData>
    {
        public FileData Convert(FormFile sourceMember, ResolutionContext context)
        {
            if (sourceMember == null) return null;
            
            var content = new MemoryStream();
            sourceMember.CopyTo(content);
            content.Position = 0;
            return new FileData(sourceMember.FileName.FileNameWithoutExtension(), sourceMember.FileName.GetExtension(), content.Length, content);
        }
    }
}