using AutoMapper;
using Filps.Application.Requests.Files.Commands.SaveFile;
using Filps.Application.Requests.Files.Queries.DownloadFile;
using Filps.Application.Requests.Files.Queries.GetFile;
using Filps.Application.Requests.Files.Queries.OpenFile;
using Filps.Application.Requests.GoogleDrive.Queries.DownloadFile;
using Filps.Application.Requests.GoogleDrive.Queries.GetFiles;
using Filps.Mappings.Converters;
using Filps.Models.Files;

namespace Filps.Mappings.Profiles
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<GetFilesInput, GetGoogleDriveFilesQuery>();
            CreateMap<GetFileInput, DownloadGoogleDriveFileQuery>();
            CreateMap<OpenFileInput, OpenFileQuery>()
                .ForMember(dest => dest.File, options => options.ConvertUsing(new FileConverter()));
            
            CreateMap<SaveFileInput, SaveFileCommand>();
            CreateMap<GetFileInput, GetFileQuery>();
            CreateMap<GetFileInput, DownloadFileQuery>();
        }
    }
}