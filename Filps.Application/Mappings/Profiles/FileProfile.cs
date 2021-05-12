using AutoMapper;
using Filps.Application.Models.Storage;
using Filps.Application.Requests.Files.Queries.GetFile;
using Filps.Application.Requests.Files.Queries.GetUserFiles;
using Filps.Common.Extensions;
using Filps.Domain.Models.Files;
using Filps.GoogleServices.Models.Drive;
using File = Google.Apis.Drive.v3.Data.File;

namespace Filps.Application.Mappings.Profiles
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<File, StorageObject>().ForMember(dest => dest.Name, 
                options => options.MapFrom(src => src.Name.FileNameWithoutExtension()));
            CreateMap<StorageContent, GetFilesResponse>();
            CreateMap<FileMetadata, Domain.Models.Files.File>();
            CreateMap<GetUserFilesQuery, GetFilesFilters>();
        }
    }
}